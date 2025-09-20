using Application.Core;
using Application.Interfaces;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles.Commands
{
    public class SetMainPhoto
    {
        public class Command : IRequest<Results<Unit>>
        {
            public required string PhotoId { get; set; }

        }

        public class Handler(AppDbContext context, IUserAccesser userAccesser)
            : IRequestHandler<Command, Results<Unit>>
        {
            public async Task<Results<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await userAccesser.GetUserWithPhotosAsync();

                var photo = user.Photos.FirstOrDefault(x => x.Id == request.PhotoId);

                if (photo == null) return Results<Unit>.Failure("Cannot find photo", 400);

                user.ImageUrl = photo.Url;

                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                return result
                    ? Results<Unit>.Success(Unit.Value)
                    : Results<Unit>.Failure("Problem changing photo", 400);
            }
        }
    }
}
