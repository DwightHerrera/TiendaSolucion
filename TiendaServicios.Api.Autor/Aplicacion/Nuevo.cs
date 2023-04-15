using FluentValidation;
using MediatR;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }
        public class EjecutValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutValidacion()
            {
                RuleFor(p => p.Nombre).NotEmpty(); //No acaepta valores nulos
                RuleFor(p => p.Apellido).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoAutor _context;
            public Manejador(ContextoAutor context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Se crea la instancia del autor-libro ligada al contexto
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid())
                };
                //Agregamos el objeto del tipo autor-libro
                _context.AutorLibros.Add(autorLibro);
                //Insertamos el valor de insercion
                var respuesta = await _context.SaveChangesAsync();

                if (respuesta > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se puede insertar el autor del libro");
            }
        }
    }
}
