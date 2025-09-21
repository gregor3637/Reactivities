using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Persistence;

namespace Application.Profiles.Commands
{
    public class AddPhoto
    {
        public class Command: IRequest<Results<Photo>>
        {
            public required IFormFile File{ get; set; } // the name of the prop has to the same as the prop we are obtaining from the request. 
            // i.e. in the request we need to have an object with name 'file'
        }

        public class Handler(IUserAccesser userAccesser, AppDbContext context,
            IPhotoService photoService) : IRequestHandler<Command, Results<Photo>>
        {
            public async Task<Results<Photo>> Handle(Command request, CancellationToken cancellationToken)
            {
                var uploadResult = await photoService.UploadPhoto(request.File);

                if (uploadResult == null) return Results<Photo>.Failure("Failed to upload photo", 400);

                var user = await userAccesser.GetUserAsync();

                var photo = new Photo
                {
                    Url = uploadResult.Url,
                    PublicId = uploadResult.PublicId,
                    UserId = user.Id,
                };

                user.ImageUrl ??= photo.Url;

                context.Photos.Add(photo);

                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                return result
                    ? Results<Photo>.Success(photo)
                    : Results<Photo>.Failure("Problem Saving photo to DB", 400);
            }
        }

    }
}
