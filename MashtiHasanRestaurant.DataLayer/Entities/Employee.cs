﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MashtiHasanRestaurant.DataLayer.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}