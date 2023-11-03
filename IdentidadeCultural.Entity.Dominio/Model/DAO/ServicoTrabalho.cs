using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IdentidadeCultural.Entity.Dominio.Model
{
    public class ServicoTrabalho
    {
        /*
        public ServicoTrabalho(Guid id, Guid? id_Usuario, string titulo, string categoria, string descricao, string foto, DateTime criacao)
        {
            Id = id;
            Usuario_Id = id_Usuario;
            Titulo = titulo;
            Categoria = categoria;
            Descricao = descricao;
            Foto = foto;
            Criacao = criacao;
        }*/
        public ServicoTrabalho()
        {
        }
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public DateTime Criacao { get; set; }

        public Guid? UsuarioId { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }

    }
}