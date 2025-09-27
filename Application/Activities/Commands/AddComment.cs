using Application.Activities.DTOs;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Activities.Commands
{
    public class AddComment
    {
        public class Command : IRequest<Results<CommentDto>>
        {
            public required string Body { get; set; }
            public required string ActivityId { get; set; }
        }

        public class Handler(AppDbContext context, IMapper mapper, IUserAccesser userAccesser)
       : IRequestHandler<Command, Results<CommentDto>>
        {
            public async Task<Results<CommentDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                    .FirstOrDefaultAsync(x => x.Id == request.ActivityId, cancellationToken);

                if (activity == null) return Results<CommentDto>.Failure("Could not find activity:", 404);

                var user = await userAccesser.GetUserAsync();

                var comment = new Comment
                {
                    UserId = user.Id,
                    ActivityId = activity.Id,
                    Body = request.Body,
                };

                activity.Comments.Add(comment);

                var result = await context.SaveChangesAsync(cancellationToken) >0;

                return result
                    ? Results<CommentDto>.Success(mapper.Map<CommentDto>(comment))
                    : Results<CommentDto>.Failure("Failed to add comment", 400);
            }
        }
    }
}
