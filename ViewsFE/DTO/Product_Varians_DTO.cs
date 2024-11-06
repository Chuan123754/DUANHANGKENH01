namespace appAPI.DTO
{
    public class Product_Varians_DTO
    {
        public long Id { get; set; }
        public string Description { get; set; }
        // thuộc tính của nó 
        public long Material_Id {  get; set; }
        public long Style_Id { get; set; }
        public long Text_Tile_Technologies_Id { get; set; }

    }
}
