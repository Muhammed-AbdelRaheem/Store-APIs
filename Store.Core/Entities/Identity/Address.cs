﻿namespace Store.Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }



    }
}