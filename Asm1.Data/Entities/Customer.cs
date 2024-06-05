﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm1.Data.Entities;
[Table("Customer", Schema = "dbo")]
public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerFullName { get; set; }

    public string Telephone { get; set; }

    public string EmailAddress { get; set; } = null!;

    public DateTime CustomerBirthday { get; set; }

    public byte CustomerStatus { get; set; }

    public string Password { get; set; }

    public virtual ICollection<BookingReservation> BookingReservations { get; set; } = new List<BookingReservation>();
}