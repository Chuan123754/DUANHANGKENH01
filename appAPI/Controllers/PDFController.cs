using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.Layout.Borders;
using appAPI.Repository;
using appAPI.Models;
using appAPI.IRepository;
using iText.IO.Image;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
        private readonly IRepository<Orders> _orderRepository;
        private readonly IRepository<Order_details> _orderDetailsRepository;
        private readonly IRepository<Users> _userRepository;
        private readonly IProductAttributesRepository _productAttrubuteRepo;

        public PDFController(
            IRepository<Orders> orderRepository,
            IRepository<Order_details> orderDetailsRepository,
            IRepository<Users> userRepository,
            IProductAttributesRepository productAttrubuteRepo)
        {
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _userRepository = userRepository;
            _productAttrubuteRepo = productAttrubuteRepo;
        }


        [HttpGet("generate")]
        public async Task<IActionResult> GenerateInvoice([FromQuery] int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null) return NotFound("Không tìm thấy đơn hàng.");

            var user = _userRepository.GetById(order.User_id ?? 0);

            var orderDetails = _orderDetailsRepository.Find(od => od.OrderId == orderId).ToList();

            using (var stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, PageSize.A4);
                document.SetMargins(30, 25, 30, 25);

                // Load font  
                string fontPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Fonts", "Roboto-Regular.ttf");
                if (!System.IO.File.Exists(fontPath)) return BadRequest("Font không tồn tại tại đường dẫn: " + fontPath);

                PdfFont fontNormal = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);
                PdfFont fontBold = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

                string imagePath = @"D:\DATN\DUANHANGKENH01\appAPI\FileMedia\Logohk.jpg"; 
                if (!System.IO.File.Exists(imagePath)) return BadRequest("Hình ảnh không tồn tại tại đường dẫn: " + imagePath);

                Image logoImage = new Image(ImageDataFactory.Create(imagePath)).SetWidth(50).SetHeight(50);

                Table headerTable = new Table(3).UseAllAvailableWidth(); 
                headerTable.AddCell(new Cell().Add(logoImage).SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                headerTable.AddCell(new Cell().Add(new Paragraph("HÓA ĐƠN BÁN HÀNG").SetFont(fontBold).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER)).SetBorder(Border.NO_BORDER));
                headerTable.AddCell(new Cell().Add(new Paragraph($"Số (No.): {order.Id}").SetFont(fontNormal).SetFontSize(9).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                document.Add(headerTable);

                document.Add(new Paragraph("VAT INVOICE").SetFont(fontNormal).SetFontSize(9).SetTextAlignment(TextAlignment.CENTER));
                document.Add(new Paragraph($"Ngày (Date): {DateTime.Now:dd/MM/yyyy HH:mm}\n\n").SetFont(fontNormal).SetFontSize(9).SetTextAlignment(TextAlignment.CENTER));

                // Thông tin Người bán
                document.Add(new Paragraph("Tên đơn vị bán hàng (Seller): CÔNG TY CỔ PHẦN THẢM LEN HÀNG KÊNH").SetFont(fontNormal).SetFontSize(9));
                document.Add(new Paragraph("Mã số thuế (Tax code): 0201558210").SetFont(fontNormal).SetFontSize(9));
                document.Add(new Paragraph("Địa chỉ (Address): Đội 7, Xã An Thọ, Huyện An Lão, Thành phố Hải Phòng, Việt Nam").SetFont(fontNormal).SetFontSize(9));
                document.Add(new Paragraph("Số tài khoản (A/C No.): 13136969301 tại Ngân hàng TMCP Tiên Phong - CN Hoàn Kiếm").SetFont(fontNormal).SetFontSize(9));
                document.Add(new Paragraph("----------------------------------------------------------------------------------------------------------------------------------------"));

                // Thông tin Người mua
                if (user != null)
                {
                    document.Add(new Paragraph($"Họ tên người mua hàng (Buyer): {user.Name}").SetFont(fontNormal).SetFontSize(9));
                    document.Add(new Paragraph($"Email (E-mail): {user.Email}").SetFont(fontNormal).SetFontSize(9)); 
                    document.Add(new Paragraph($"Số điện thoại (Phone): {user.Phone}").SetFont(fontNormal).SetFontSize(9));
                }
                else
                {
                    document.Add(new Paragraph("Họ tên người mua hàng (Buyer): KHÁCH LẺ").SetFont(fontNormal).SetFontSize(9));
                    document.Add(new Paragraph("Email (E-mail): ").SetFont(fontNormal).SetFontSize(9));
                    document.Add(new Paragraph("Số điện thoại (Phone): #N/A ").SetFont(fontNormal).SetFontSize(9));
                }

                // Bảng chi tiết thanh toán
                Table paymentTable = new Table(new float[] { 1, 1 }).UseAllAvailableWidth();
                paymentTable.AddCell(new Cell().Add(new Paragraph($"Hình thức thanh toán (Payment method): {order.TypePayment}").SetFont(fontNormal).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                paymentTable.AddCell(new Cell().Add(new Paragraph("Số tài khoản (A/C No.): 0358384108").SetFont(fontNormal).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                document.Add(paymentTable);

                document.Add(new Paragraph("----------------------------------------------------------------------------------------------------------------------------------------"));

                Table itemTable = new Table(new float[] { 0.7f, 1.5f, 1.2f, 1.2f, 1, 1.5f, 1, 1.5f }).UseAllAvailableWidth();
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("STT").SetFont(fontBold).SetFontSize(9)));
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Tên sản phẩm").SetFont(fontBold).SetFontSize(9)));
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Màu sắc").SetFont(fontBold).SetFontSize(9)));
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Size").SetFont(fontBold).SetFontSize(9)));
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Đơn giá").SetFont(fontBold).SetFontSize(9)));
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Giá chiết khấu").SetFont(fontBold).SetFontSize(9)));
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Số lượng").SetFont(fontBold).SetFontSize(9)));
                itemTable.AddHeaderCell(new Cell().Add(new Paragraph("Thành tiền").SetFont(fontBold).SetFontSize(9)));

                int index = 1;
                decimal totalAmount = 0;              // Tổng tiền hàng đã giảm
                decimal totalAmountWithoutDiscount = 0; // Tổng tiền hàng chưa giảm
                decimal totalDiscount = 0;            // Tổng tiền đực giảm
                decimal totalPayableAmount = 0;     // tổng thanh toán
                foreach (var detail in orderDetails)
                {
                    var product = await _productAttrubuteRepo.GetProductAttributesById(detail.Product_Attribute_Id);
                    if (product != null)
                    {
                        decimal unitPrice = detail.UnitPrice?? 0; // giá gốc
                        decimal discountPrice = detail.TotalDiscount ?? unitPrice;
                        if (discountPrice > unitPrice)
                        {
                            discountPrice = unitPrice;
                        }
                        string discountPriceText = discountPrice < unitPrice
                                               ? $"{discountPrice:0,0}"
                                               : "#N/A"; // Hiển thị giá giảm nếu khác giá gốc
                        decimal appliedPrice = discountPrice > 0 ? discountPrice : unitPrice; // nếu giá giảm null thì giá giảm = giá gốc

                        decimal amount = detail.Quantity * appliedPrice; // giá tổng giá đã giảm
                        decimal amountWithoutDiscount = detail.Quantity * unitPrice;  // giá ban đầu

                        decimal discountAmount = (unitPrice - discountPrice) * detail.Quantity;

                        itemTable.AddCell(new Cell().Add(new Paragraph(index.ToString()).SetFont(fontNormal).SetFontSize(9)));
                        itemTable.AddCell(new Cell().Add(new Paragraph(product.Posts.Title).SetFont(fontNormal).SetFontSize(9)));
                        itemTable.AddCell(new Cell().Add(new Paragraph(product.Color.Title).SetFont(fontNormal).SetFontSize(9)));
                        itemTable.AddCell(new Cell().Add(new Paragraph(product.Size.Title).SetFont(fontNormal).SetFontSize(9)));
                        itemTable.AddCell(new Cell().Add(new Paragraph($"{unitPrice:0,0}").SetFont(fontNormal).SetFontSize(9)));
                        itemTable.AddCell(new Cell().Add(new Paragraph(discountPriceText).SetFont(fontNormal).SetFontSize(9)));
                        itemTable.AddCell(new Cell().Add(new Paragraph(detail.Quantity.ToString()).SetFont(fontNormal).SetFontSize(9)));
                        itemTable.AddCell(new Cell().Add(new Paragraph($"{amount:0,0}").SetFont(fontNormal).SetFontSize(9)));

                        totalAmount += amount; // Tổng tiền hàng đã giảm
                        totalAmountWithoutDiscount += amountWithoutDiscount; // Tổng tiền hàng chưa giảm
                        totalDiscount += discountAmount; // Tổng tiền giảm
                        totalPayableAmount += amount;

                        index++;
                    }
                }
                document.Add(itemTable);

                document.Add(new Paragraph("----------------------------------------------------------------------------------------------------------------------------------------"));

                // Tổng tiền
                //decimal totalPayment = totalAmount * 1.1m;
                string amountInWords = ConvertNumberToWords(order.Totalmoney??0);

                Table totalTable = new Table(1).UseAllAvailableWidth();
                totalTable.AddCell(new Cell().Add(new Paragraph($"Cộng tiền hàng chưa giảm (Total): {totalAmountWithoutDiscount:0,0} VND").SetFont(fontNormal).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                totalTable.AddCell(new Cell().Add(new Paragraph($"Cộng tiền hàng đã giảm giá (Total amount): {totalAmount:0,0} VND").SetFont(fontNormal).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                totalTable.AddCell(new Cell().Add(new Paragraph($"Tổng tiền được chiết khấu : {totalDiscount:0,0} VND").SetFont(fontNormal).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                totalTable.AddCell(new Cell().Add(new Paragraph($"Phí giao hàng ( FeeShipping): {order.FeeShipping:0,0} VND").SetFont(fontBold).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                totalTable.AddCell(new Cell().Add(new Paragraph($"Giảm giá từ Voucher ( Voucher): {order.TotalVoucher:0,0} VND").SetFont(fontBold).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                totalTable.AddCell(new Cell().Add(new Paragraph($"Tổng cộng tiền thanh toán (Total payment): {order.Totalmoney:0,0} VND").SetFont(fontBold).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                document.Add(totalTable);

                document.Add(new Paragraph($"Số tiền viết bằng chữ (Amount in words): {amountInWords}").SetFont(fontNormal).SetFontSize(9));

                // Ký tên
                Table signatureTable = new Table(new float[] { 1, 1 }).UseAllAvailableWidth();
                signatureTable.AddCell(new Cell().Add(new Paragraph("NGƯỜI MUA HÀNG (Buyer)\n(Ký, ghi rõ họ, tên)").SetFont(fontNormal).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                signatureTable.AddCell(new Cell().Add(new Paragraph("NGƯỜI BÁN HÀNG (Seller)\n(Ký, đóng dấu, ghi rõ họ, tên)").SetFont(fontNormal).SetFontSize(9)).SetBorder(Border.NO_BORDER));
                document.Add(signatureTable);

                document.Close();

                byte[] bytes = stream.ToArray();
                return File(bytes, "application/pdf", $"hoa_don_{orderId}.pdf");
            }
        }
        private static string ConvertNumberToWords(decimal number)
        {
            string[] units = { "", "nghìn", "triệu", "tỷ" };
            string[] numbers = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

            if (number == 0) return "không đồng";

            string words = "";
            int unitIndex = 0;

            while (number > 0)
            {
                int part = (int)(number % 1000);
                if (part > 0)
                {
                    string partWords = "";
                    int hundreds = part / 100;
                    int tens = (part % 100) / 10;
                    int unitsPart = part % 10;

                    if (hundreds > 0)
                    {
                        partWords += numbers[hundreds] + " trăm ";
                        if (tens == 0 && unitsPart > 0) partWords += "lẻ ";
                    }

                    if (tens > 1)
                    {
                        partWords += numbers[tens] + " mươi ";
                        if (unitsPart > 0) partWords += numbers[unitsPart] + " ";
                    }
                    else if (tens == 1)
                    {
                        partWords += "mười ";
                        if (unitsPart > 0) partWords += numbers[unitsPart] + " ";
                    }
                    else if (unitsPart > 0)
                    {
                        partWords += numbers[unitsPart] + " ";
                    }

                    partWords += units[unitIndex] + " ";
                    words = partWords + words;
                }

                number /= 1000;
                unitIndex++;
            }

            return words.Trim() + " đồng";
        }
    }
}
