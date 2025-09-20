using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core;
using Persistence;
using Application.Interfaces;

namespace Application.Profiles.Commands
{
    public class DeletePhoto
    {
        public class Command: IRequest<Results<Unit>>
        {
            public required string PhotoId { get; set;}

        }

        public class Handler(AppDbContext context, IUserAccesser userAccesser, IPhotoService photoService) 
            : IRequestHandler<Command, Results<Unit>>
        {
            public async Task<Results<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await userAccesser.GetUserWithPhotosAsync();

                var photo =  user.Photos.FirstOrDefault(x => x.Id == request.PhotoId);

                if (photo == null) return Results<Unit>.Failure("Cannot find photo", 400);

                if(photo.Url == user.ImageUrl) return Results<Unit>.Failure("Cannot delete main photo", 400);

                var deleteResult =  await photoService.DeletePhoto(photo.PublicId);

                user.Photos.Remove(photo);

                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                return result
                    ? Results<Unit>.Success(Unit.Value)
                    : Results<Unit>.Failure("Problem deleting photo", 400);
            }
        }
    }
}
