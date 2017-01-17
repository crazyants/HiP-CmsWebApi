﻿using Api.Data;
using Api.Models;
using Api.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Managers
{
    public class DocumentManager : BaseManager
    {
        public DocumentManager(CmsDbContext dbContext) : base(dbContext) { }


        /// <exception cref="InvalidOperationException">The input sequence contains more than one element. -or- The input sequence is empty.</exception>
        public virtual Document GetDocumentById(int topicId)
        {
            return dbContext.Documents.Include(d => d.Updater).Single(d => (d.TopicId == topicId));
        }

        internal EntityResult UpdateDocument(int topicId, int userId, String htmlContent)
        {
            Topic topic;
            try
            {
                topic = dbContext.Topics.Include(t => t.Document).Single(t => t.Id == topicId);
            }
            catch (InvalidOperationException)
            {
                return EntityResult.Error("Unknown Topic");
            }
            // already exitsts

            try
            {
                var document = GetDocumentById(topicId);
                // Yes -> delete Old at first
                dbContext.Remove(document);
            }
            catch (InvalidOperationException)
            {
                // no -> create new.
            }
            try
            {
                Document document = new Document(userId, htmlContent);

                dbContext.Add(document);
                dbContext.SaveChanges();

                return EntityResult.Successfull();
            }
            catch (Exception e)
            {
                return EntityResult.Error(e.Message);
            }
        }


        public bool DeleteDocument(int topicId)
        {
            try
            {
                var document = GetDocumentById(topicId);
                dbContext.Remove(document);
                dbContext.SaveChanges();
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}
