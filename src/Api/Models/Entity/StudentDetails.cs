﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaderbornUniversity.SILab.Hip.CmsApi.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaderbornUniversity.SILab.Hip.CmsApi.Models.Entity
{
    public class StudentDetails
    {
        // ReSharper disable once UnusedMember.Global
        public StudentDetails() { }

        public StudentDetails(string userId, StudentFormModel model)
        {
            UserId = userId;
            Discipline = model.Discipline;
            CurrentDegree = model.CurrentDegree;
            CurrentSemester = model.CurrentSemester;
        }

        [Required, ForeignKey("UserId")]
        public string UserId { get; set; }

        public string Discipline { get; set; }

        public string CurrentDegree { get; set; }

        public short CurrentSemester { get; set; }

        public static void ConfigureModel(EntityTypeBuilder<StudentDetails> entityBuilder)
        {
            entityBuilder.HasKey(d => new { d.UserId });
        }
    }
}
