﻿using System;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Models
{
    public class CartIndexViewModel
    {
        public  Cart    Cart { get; set; }
        public  String  ReturnUrl { get; set; }
    }
}