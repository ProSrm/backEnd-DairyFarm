﻿namespace dairyFarm.Model
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }
    }
}
