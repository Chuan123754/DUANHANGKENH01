using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using appAPI.Repository;
using appAPI.Models;
using System.IO;
using System.Linq;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
        private readonly IRepository<Orders> _orderRepository;
        private readonly IRepository<Order_details> _orderDetailsRepository;
        private readonly IRepository<Users> _userRepository;
        private readonly IRepository<Product_Attributes> _productAttributesRepository;

        public PDFController(
            IRepository<Orders> orderRepository,
            IRepository<Order_details> orderDetailsRepository,
            IRepository<Users> userRepository,
            IRepository<Product_Attributes> productAttributesRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _userRepository = userRepository;
            _productAttributesRepository = productAttributesRepository;
        }

        public static string ConvertNumberToWords(decimal number)
        {
            string[] units = { "", "mươi", "trăm", "nghìn", "triệu", "tỷ" };
            string[] numbers = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

            if (number == 0)
                return "không đồng";

            string words = "";
            bool isNegative = false;

            if (number < 0)
            {
                isNegative = true;
                number = Math.Abs(number);
            }

            int partCount = 0;
            while (number > 0)
            {
                int part = (int)(number % 1000);
                if (part > 0)
                {
                    string partWords = "";
                    int hundred = part / 100;
                    int ten = (part % 100) / 10;
                    int unit = part % 10;

                    if (hundred > 0)
                    {
                        partWords += numbers[hundred] + " trăm ";
                        if (ten == 0 && unit > 0)
                            partWords += "lẻ ";
                    }

                    if (ten > 1)
                    {
                        partWords += numbers[ten] + " mươi ";
                        if (unit == 1)
                            partWords += "mốt ";
                        else if (unit > 0)
                            partWords += numbers[unit] + " ";
                    }
                    else if (ten == 1)
                    {
                        partWords += "mười ";
                        if (unit > 0)
                            partWords += numbers[unit] + " ";
                    }
                    else if (ten == 0 && unit > 0)
                    {
                        partWords += numbers[unit] + " ";
                    }

                    partWords += units[partCount] + " ";
                    words = partWords + words;
                }

                partCount++;
                number = (int)(number / 1000);
            }

            words = words.Trim() + " đồng";
            return isNegative ? "âm " + words : words;
        }


        [HttpGet("generate")]
        public IActionResult GenerateInvoice([FromQuery] int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null) return NotFound("Không tìm thấy đơn hàng.");

            var user = _userRepository.GetById(order.User_id ?? 0);
            if (user == null) return NotFound("Không tìm thấy người dùng.");

            var orderDetails = _orderDetailsRepository.Find(od => od.OrderId == orderId).ToList();

            using (var stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                // Load font
                string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "Fonts", "Roboto-Regular.ttf");
                if (!System.IO.File.Exists(fontPath)) return BadRequest("Font không tồn tại tại đường dẫn: " + fontPath);

                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font fontNormal = new Font(baseFont, 10, Font.NORMAL);
                Font fontBold = new Font(baseFont, 10, Font.BOLD);

                // Thông tin Header
                PdfPTable headerTable = new PdfPTable(3);
                headerTable.WidthPercentage = 100;
                headerTable.AddCell(new PdfPCell(new Phrase("HangKenh", fontBold)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = Rectangle.NO_BORDER });
                headerTable.AddCell(new PdfPCell(new Phrase("HÓA ĐƠN GIÁ TRỊ GIA TĂNG", fontBold)) { HorizontalAlignment = Element.ALIGN_CENTER, Border = Rectangle.NO_BORDER });
                headerTable.AddCell(new PdfPCell(new Phrase($"Ký hiệu (Series): 1K24TAA\nSố (No.): {order.Id}", fontNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = Rectangle.NO_BORDER });
                document.Add(headerTable);

                document.Add(new Paragraph("VAT INVOICE", fontNormal) { Alignment = Element.ALIGN_CENTER });
                document.Add(new Paragraph($"Ngày (Date): {DateTime.Now:dd/MM/yyyy}\n\n", fontNormal) { Alignment = Element.ALIGN_CENTER });

                // Thông tin Người bán và Người mua
                document.Add(new Paragraph("Tên đơn vị bán hàng (Seller): TRƯỜNG CAO ĐẲNG FPT POLYTECHNIC", fontNormal));
                document.Add(new Paragraph("Mã số thuế (Tax code): 0102635866", fontNormal));
                document.Add(new Paragraph("Địa chỉ (Address): Km12, đường Cầu Diễn, P.Phúc Diễn, Q.Bắc Từ Liêm, Hà Nội", fontNormal));
                document.Add(new Paragraph("Số tài khoản (A/C No.): 13136969301 tại Ngân hàng TMCP Tiên Phong - CN Hoàn Kiếm", fontNormal));
                document.Add(new Paragraph("----------------------------------------------------------------------------------------------------------------------------------------"));

                // Thông tin người mua hàng
                document.Add(new Paragraph($"Họ tên người mua hàng (Buyer): {user.Name}", fontNormal));
                document.Add(new Paragraph($"Địa chỉ (Address): {user.Address}", fontNormal));
                document.Add(new Paragraph($"Số điện thoại (Phone): {user.Phone}", fontNormal));

                // Bảng Payment Method và Account Number trên cùng một dòng
                PdfPTable paymentTable = new PdfPTable(2);
                paymentTable.WidthPercentage = 100;
                paymentTable.AddCell(new PdfPCell(new Phrase("Hình thức thanh toán (Payment method): Chuyển khoản", fontNormal)) { Border = Rectangle.NO_BORDER });
                paymentTable.AddCell(new PdfPCell(new Phrase("Số tài khoản (A/C No.): 13136969301", fontNormal)) { Border = Rectangle.NO_BORDER });
                document.Add(paymentTable);

                document.Add(new Paragraph("Ngân hàng (Bank): [Tên ngân hàng của bạn]", fontNormal));
                document.Add(new Paragraph("----------------------------------------------------------------------------------------------------------------------------------------"));

                // Bảng chi tiết sản phẩm với chiều rộng tùy chỉnh cho từng cột
                PdfPTable itemTable = new PdfPTable(7) { WidthPercentage = 100 };
                float[] columnWidths = { 1f, 3f, 2f, 2f, 2f, 1.5f, 2f };
                itemTable.SetWidths(columnWidths);

                itemTable.AddCell(new Phrase("STT (No.)", fontBold));
                itemTable.AddCell(new Phrase("Mã sản phẩm SKU", fontBold));
                itemTable.AddCell(new Phrase("Đơn giá", fontBold));
                itemTable.AddCell(new Phrase("Giá Giảm (Nếu có)", fontBold));
                itemTable.AddCell(new Phrase("Giá cuối", fontBold));
                itemTable.AddCell(new Phrase("Số lượng", fontBold));
                itemTable.AddCell(new Phrase("Thành tiền", fontBold));

                int index = 1;
                decimal totalAmount = 0;
                foreach (var detail in orderDetails)
                {
                    var product = _productAttributesRepository.GetById(detail.Product_Attribute_Id);
                    if (product != null)
                    {
                        decimal unitPrice = product.Regular_price ?? 0;
                        decimal discountPrice = product.Sale_price ?? 0;
                        decimal finalPrice = unitPrice - discountPrice;
                        decimal amount = detail.Quantity * finalPrice;

                        itemTable.AddCell(new Phrase(index.ToString(), fontNormal));
                        itemTable.AddCell(new Phrase(product.SKU, fontNormal));
                        itemTable.AddCell(new Phrase(product.Regular_price.ToString(), fontNormal));
                        itemTable.AddCell(new Phrase(discountPrice > 0 ? $"{discountPrice:0,0}" : "0", fontNormal));
                        itemTable.AddCell(new Phrase($"{finalPrice:0,0}", fontNormal));
                        itemTable.AddCell(new Phrase(detail.Quantity.ToString(), fontNormal));
                        itemTable.AddCell(new Phrase($"{amount:0,0}", fontNormal));

                        totalAmount += amount;
                        index++;
                    }
                }

                document.Add(itemTable);

                document.Add(new Paragraph("----------------------------------------------------------------------------------------------------------------------------------------"));

                // Tính tổng tiền thanh toán
                decimal totalPayment = totalAmount * 1.1m;
                string amountInWords = ConvertNumberToWords(totalPayment);

                // Footer tổng tiền với số tiền viết bằng chữ, sử dụng các Paragraph riêng lẻ cho từng dòng
                PdfPTable totalTable = new PdfPTable(1);
                totalTable.WidthPercentage = 100;

                // Tạo từng Paragraph với khoảng cách sau mỗi dòng
                Paragraph totalAmountParagraph = new Paragraph($"Cộng tiền hàng (Total amount): {totalAmount:0,0} VND", fontNormal)
                {
                    SpacingAfter = 5f 
                };
                Paragraph vatRateParagraph = new Paragraph("Thuế suất GTGT (VAT rate): 10%", fontNormal)
                {
                    SpacingAfter = 5f
                };
                Paragraph vatAmountParagraph = new Paragraph($"Tiền thuế GTGT (VAT amount): {(totalAmount * 0.1m):0,0} VND", fontNormal)
                {
                    SpacingAfter = 5f
                };
                Paragraph totalPaymentParagraph = new Paragraph($"Tổng cộng tiền thanh toán (Total payment): {totalPayment:0,0} VND", fontNormal)
                {
                    SpacingAfter = 5f
                };
                Paragraph amountInWordsParagraph = new Paragraph($"Số tiền viết bằng chữ (Amount in words): {amountInWords}", fontNormal)
                {
                    SpacingAfter = 15f
                };

                // Thêm các Paragraph vào bảng
                totalTable.AddCell(new PdfPCell(totalAmountParagraph) { Border = Rectangle.NO_BORDER });
                totalTable.AddCell(new PdfPCell(vatRateParagraph) { Border = Rectangle.NO_BORDER });
                totalTable.AddCell(new PdfPCell(vatAmountParagraph) { Border = Rectangle.NO_BORDER });
                totalTable.AddCell(new PdfPCell(totalPaymentParagraph) { Border = Rectangle.NO_BORDER });
                totalTable.AddCell(new PdfPCell(amountInWordsParagraph) { Border = Rectangle.NO_BORDER });

                // Thêm bảng tổng tiền vào tài liệu
                document.Add(totalTable);
                document.Add(new Paragraph("\n"));


                // Ký tên
                PdfPTable signatureTable = new PdfPTable(2) { WidthPercentage = 100 };
                signatureTable.AddCell(new PdfPCell(new Phrase("NGƯỜI MUA HÀNG (Buyer)\n(Ký, ghi rõ họ, tên)", fontNormal)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                signatureTable.AddCell(new PdfPCell(new Phrase("NGƯỜI BÁN HÀNG (Seller)\n(Ký, đóng dấu, ghi rõ họ, tên)", fontNormal)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                document.Add(signatureTable);

                document.Close();

                // Trả file PDF về client
                byte[] file = stream.ToArray();
                return File(file, "application/pdf", $"hoa_don_{orderId}.pdf");
            }
        }
    }
}
