﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Consulta
    {
        public class ListaAutor : IRequest<List<AutorDto>>
        {
        }
            public class Manejador : IRequestHandler<ListaAutor, List<AutorDto>>
            {
                private readonly ContextoAutor _context;
                private readonly IMapper _mapper;

                public Manejador(ContextoAutor context, IMapper mapper)
                {
                    this._context = context;
                    this._mapper = mapper;
                }

                public async Task<List<AutorDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
                {
                    var autores = await _context.AutorLibros.ToListAsync();
                    var autoresDto = _mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);
                    return autoresDto;
                }
            }
        }
    }

