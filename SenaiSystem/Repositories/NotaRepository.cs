﻿using Microsoft.EntityFrameworkCore;
using SenaiSystem.Context;
using SenaiSystem.DTOs;
using SenaiSystem.Interfaces;
using SenaiSystem.Models;
using SenaiSystem.ViewModels;

namespace SenaiSystem.Repositories;

public class NotaRepository : INotaRepository
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly SenaiSystemContext _context;
    public NotaRepository(SenaiSystemContext context, ICategoriaRepository categoriaRepository)
    {
        _context = context;
        _categoriaRepository = categoriaRepository;
    }
    public List<ListarNotaViewModel> ListarTodos()
    {
        return _context.Nota
            .Include(n => n.NotaCategoria)
            .ThenInclude(nC => nC.IdCategoriaNavigation)
            .Select(n => new ListarNotaViewModel
            {
                IdNota = n.IdNota,
                Titulo = n.Titulo,
                Imagem = n.Imagem,
                Conteudo = n.Conteudo,
                DataCriacao = n.DataCriacao,
                DataModificacao = n.DataModificacao,
                Arquivada = n.Arquivada,
                Prioridade = n.Prioridade,
                Categorias = n.NotaCategoria.Select(
                    nC => new CategoriaViewModel
                    {
                        IdCategoria = nC.IdCategoriaNavigation.IdCategoria,
                        Nome = nC.IdCategoriaNavigation.Nome
                    }).ToList()
            }).ToList();
    }
    public List<ListarNotaViewModel> ListarTodasArquivadas()
    {
        return _context.Nota
            .Include(n => n.NotaCategoria)
            .ThenInclude(nC => nC.IdCategoriaNavigation)
            .Select(n => new ListarNotaViewModel
            {
                IdNota = n.IdNota,
                Titulo = n.Titulo,
                Imagem = n.Imagem,
                Conteudo = n.Conteudo,
                DataCriacao = n.DataCriacao,
                DataModificacao = n.DataModificacao,
                Arquivada = n.Arquivada,
                Prioridade = n.Prioridade,
                Categorias = n.NotaCategoria.Select(
                    nC => new CategoriaViewModel
                    {
                        IdCategoria = nC.IdCategoriaNavigation.IdCategoria,
                        Nome = nC.IdCategoriaNavigation.Nome
                    }).ToList()
            }).Where(n => n.Arquivada == true).ToList();
    }
    public Nota? BuscarPorId(int id)
    {
        return _context.Nota.FirstOrDefault(n => n.IdNota == id);
    }
    public CadastroEditarNotaDto? Cadastrar(CadastroEditarNotaDto nota)
    {
        //1- percorrer a lista de categorias
        //1.1 - verificar se a categoria existe
        //1.2 - se ela ja existe vou ter que pegar o id dela
        //1.2 - se não existe, vou ter que cadastrar ela


        List<int> idCategorias = new List<int>();


        //Percorrer a lista de categorias
        foreach (var item in nota.Categorias)
        {
            //Verificar se a categoria existe
            var tag = _categoriaRepository.BuscarPorUsuarioeId(nota.IdUsuario, item);

            //Caso nao exista - Crio uma nova categoria
            if (tag == null)
            {
                tag = new Categoria
                {
                    Nome = item,
                    IdUsuario = nota.IdUsuario
                };

                _context.Add(tag);
                _context.SaveChanges();

                // todo: cadastrar a categoria
            }

            idCategorias.Add(tag.IdCategoria);

            //Cadastrar a categoria

            var novaNota = new Nota
            {
                 Titulo = nota.Titulo,
                Imagem = nota.Imagem,
                Conteudo = nota.Conteudo,
                DataCriacao = DateTime.Now,
                DataModificacao = DateTime.Now,
                Arquivada = false,
                Prioridade = nota.Prioridade,
                IdUsuario = nota.IdUsuario
            };

            _context.Add(novaNota);
            _context.SaveChanges();


            // Cadastrar a notaCategoria 

            foreach (var id in idCategorias)
            {
                var notaCategoria = new NotaCategoria
                {
                    IdNota = novaNota.IdNota,
                    IdCategoria = id
                };
                _context.Add(notaCategoria);
                _context.SaveChanges();
            }

            
        }

        return nota;
    }
    public Nota? Atualizar(int id, CadastroEditarNotaDto nota)
    {
        var notaAtual = _context.Nota.FirstOrDefault(n => n.IdNota == id);
        if (notaAtual == null)
            return null;
        

        notaAtual.IdUsuario = nota.IdUsuario;
        notaAtual.Titulo = nota.Titulo;
        notaAtual.Imagem = nota.Imagem;
        notaAtual.Conteudo = nota.Conteudo;
        notaAtual.DataCriacao = nota.DataCriacao;
        notaAtual.DataModificacao = nota.DataModificacao;
        notaAtual.Arquivada = nota.Arquivada;
        notaAtual.Prioridade = nota.Prioridade;

        _context.SaveChanges();
        return notaAtual;
    }
    public Nota? Deletar(int id)
    {
        var nota = _context.Nota.FirstOrDefault(n => n.IdNota == id);
        if (nota == null)
            return null;

        _context.Nota.Remove(nota);
        _context.SaveChanges();

        return nota;
    }
    public Nota? Arquivada(int id)
    {
        var nota = _context.Nota.Find(id);
        if (nota == null)
            return null;

        nota.Arquivada = !nota.Arquivada;
        _context.SaveChanges();
        return nota;
    }


    public List<ListarNotaViewModel> BuscarPorUsuario(int id)
    {
        return _context.Nota
           .Include(n => n.NotaCategoria)
           .ThenInclude(nC => nC.IdCategoriaNavigation)
           .Where(n => n.IdUsuario == id)
           .Select(n => new ListarNotaViewModel
           {
               IdNota = n.IdNota,
               Titulo = n.Titulo,
               Imagem = n.Imagem,
               Conteudo = n.Conteudo,
               DataCriacao = n.DataCriacao,
               DataModificacao = n.DataModificacao,
               Arquivada = n.Arquivada,
               Prioridade = n.Prioridade,
               Categorias = n.NotaCategoria.Select(
                   nC => new CategoriaViewModel
                   {
                       IdCategoria = nC.IdCategoriaNavigation.IdCategoria,
                       Nome = nC.IdCategoriaNavigation.Nome
                   }).ToList()
           }).ToList();
    }
    

    public List<Nota>? BuscarPorInformacao(string texto)
    {
        var notas = _context.Nota
            .Include(n => n.NotaCategoria)
            .ThenInclude(nC => nC.IdCategoriaNavigation)
            .Where(n => n.Titulo.Contains(texto) || n.Conteudo.Contains(texto) || n.NotaCategoria.Any(nC => nC.IdCategoriaNavigation.Nome.Contains(texto)))
            .ToList();
        return notas;
    }
}
