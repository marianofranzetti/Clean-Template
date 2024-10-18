using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
          
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre no puede estar en blanco")
                .NotNull().WithMessage("El cambo no puede estar vacio")
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres");

            RuleFor(p => p.Apellido)
               .NotEmpty().WithMessage("El apellido no puede estar en blanco")
               .NotNull().WithMessage("El cambo no puede estar vacio")
               .MaximumLength(50).WithMessage("El apellido no puede exceder los 50 caracteres");

            RuleFor(p => p.VideoId)
                .NotEmpty().WithMessage("El video no puede estar vacio");
        }
    }
}
