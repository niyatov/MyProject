﻿using System.Runtime.Serialization;

namespace MyProject.Models;

public class Employee
{
    public int Id { get; set; }
    public string Payroll_Number { get; set; }
    public string Forenames { get; set; }
    public string Surname { get; set; }
    public string Date_of_Birth { get; set; }
    public int Telephone { get; set; }
    public int Mobile { get; set; }
    public string Address { get; set; }
    public string Address_2 { get; set; }
    public string Postcode { get; set; }
    public string EMail_Home { get; set; }
    public string Start_Date { get; set; }
}