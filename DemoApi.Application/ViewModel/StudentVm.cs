﻿using AutoMapper;
using DemoApi.Domain.Model;

namespace DemoApi.Application.ViewModel;
[AutoMap(typeof(Student),ReverseMap =true)]
public record StudentVm(
    long Id,
    long StudentId,
    string Name,
    string Email,
    string FatherName,
    string MotherName,
    string Address,
    string Phone,
    DateTime DateOfBirth
);

