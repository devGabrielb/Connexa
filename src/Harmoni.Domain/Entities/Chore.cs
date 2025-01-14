using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Harmoni.Domain.Commons;
using Harmoni.Domain.Enums;

namespace Harmoni.Domain.Entities
{
    public class Chore : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }

        public ChoreStatus State { get; private set; }

        public Guid? AssignedBy { get; set; }
        public Guid? AssignedTo { get; set; }

        public Guid? GroupId { get; private set; }
        public Guid? UserId { get; private set; }
        public bool IsComplete => State == ChoreStatus.Done;

        private Chore() { }
        private Chore(string title, string description, DateTime dueDate, Guid? assignedTo = null)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            AssignedTo = assignedTo;

            State = ChoreStatus.New;
        }


        public static Chore Create(string title, string description, DateTime dueDate)
        {

            var chore = new Chore(
                title,
                description,
                dueDate


            );

            return chore;
        }

        public void AssignTo(Guid memberId)
        {
            AssignedTo = memberId;
        }

        public void AddToGroup(Guid groupId, Guid MemberId)
        {
            GroupId = groupId;
            AssignedBy = MemberId;
        }

        public void AddToUser(Guid userId)
        {
            UserId = userId;
        }

        public void MarkAsDone()
        {
            State = ChoreStatus.Done;
        }


    }
}