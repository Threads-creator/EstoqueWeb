using Newtonsoft.Json;

namespace EstoqueWeb.Models 
{
    public enum TipoMensagem
    {
        Informacao,
        Erro
    }

    public class MensagemModel
    {
        public TipoMensagem Tipo {get; set;}

        public string Texto {get; set;}

        public MensagemModel(string mensagem, TipoMensagem tipo = TipoMensagem.Informacao)
        {
            this.Texto = mensagem;
            this.Tipo = tipo;
        }

        public static string Serializar(string mensagem, TipoMensagem tipo = TipoMensagem.Informacao)
        {
            var mensagemModel = new MensagemModel(mensagem, tipo);
            return JsonConvert.SerializeObject(mensagemModel);
        }

        public static MensagemModel Desserializar(string mensagem, TipoMensagem tipo = TipoMensagem.Informacao)
        {
            return JsonConvert.DeserializeObject<MensagemModel>(mensagem);
        }

    }
}