﻿using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Activities.Commands
{
    public class UpdateAttendance
    {
        public class Command : IRequest<Results<Unit>>
        {
            public required string Id { get; set; }

        }

        public class Handler(IUserAccesser userAccesser, AppDbContext context) 
            : IRequestHandler<Command, Results<Unit>>
        {
            public async Task<Results<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities
                    .Include(x => x.Attendees)
                    .ThenInclude(x => x.User)
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (activity == null) return Results<Unit>.Failure("Activity not found", 404);

                var user = await userAccesser.GetUserAsync();

                var attendance = activity.Attendees.FirstOrDefault(x => x.UserId == user.Id);
                var isHost = activity.Attendees.Any(x => x.IsHost && x.UserId == user.Id);

                if (attendance != null)
                {
                    if (isHost) activity.IsCanceled = !activity.IsCanceled;
                    else activity.Attendees.Remove(attendance);
                }
                else
                {
                    activity.Attendees.Add(new ActivityAttendee
                    {
                        UserId = user.Id,
                        ActivityId = activity.Id,
                        IsHost = false
                    });
                }

                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                return result ? Results<Unit>.Success(Unit.Value) : Results<Unit>.Failure("Problem updating the DB", 400);
            }
        }
    }
}
