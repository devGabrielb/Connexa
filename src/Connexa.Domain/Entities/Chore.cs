using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Domain.Commons;
using Connexa.Domain.Enums;

namespace Connexa.Domain.Entities
{
    public class Chore : Entity
    {
        private readonly List<Comment> _comments = [];

        public Guid OwnerId { get; private set; }
        public Guid? AssignTo { get; private set; }
        public Guid? GroupId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }

        public ChoreStatus State { get; private set; }

        public IReadOnlyCollection<Comment> Comments => [.. _comments];
        public bool IsComplete => State == ChoreStatus.Done;

        private Chore(Guid ownerId, string title, string description, DateTime dueDate)
        {
            OwnerId = ownerId;
            Title = title;
            Description = description;
            DueDate = dueDate;
            State = ChoreStatus.New;
        }

        public static Chore Create(Guid ownerId, string title, string description, DateTime dueDate)
        {

            var chore = new Chore(
                ownerId,
                title,
                description,
                dueDate
            );

            return chore;
        }


        public void AddToGroup(Guid groupId)
        {

            if (!GroupId.HasValue)
            {
                GroupId = groupId;
            }
        }

        public void AddComment(string content, Guid userId)
        {
            var comment = Comment.Create(content, userId, Id);
            _comments.Add(comment);
        }

    }
}