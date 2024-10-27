using System;
using System.Collections.Generic;
using System.Linq;

using Connexa.Domain.Commons;

namespace Connexa.Domain.Entities
{
    public class Comment : Entity
    {
        public string Content { get; private set; }
        public Guid UserId { get; private set; }
        public Guid ChoreId { get; private set; }

        private Comment(string content, Guid userId, Guid choreId)
        {
            Content = content;
            UserId = userId;
            ChoreId = choreId;
        }

        public static Comment Create(string content, Guid userId, Guid choreId)
        {
            var comment = new Comment(content, userId, choreId);
            return comment;
        }

    }
}