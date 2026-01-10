namespace FoodChallenge.Common.Entities
{
    public class Resposta
    {
        public bool Sucesso { get; set; }

        public IEnumerable<string> Mensagens { get; set; }

        public static Resposta ComSucesso(IEnumerable<string> mensagens = null)
        {
            return new Resposta()
            {
                Sucesso = true,
                Mensagens = mensagens
            };
        }

        public static Resposta ComSucesso(string menssage)
        {
            return new Resposta
            {
                Sucesso = true,
                Mensagens = ConverterMensagemLista(menssage)
            };
        }

        public static Resposta ComFalha(IEnumerable<string> message)
        {
            return new Resposta
            {
                Sucesso = false,
                Mensagens = message
            };
        }

        public static Resposta ComFalha(string menssage)
        {
            return new Resposta
            {
                Sucesso = false,
                Mensagens = ConverterMensagemLista(menssage)
            };
        }

        protected static List<string> ConverterMensagemLista(string message)
        {
            List<string> mensagens = message?.Split(";").ToList();
            mensagens?.RemoveAll(e => string.IsNullOrEmpty(e));

            return mensagens;
        }
    }

    public class Resposta<T> : Resposta
    {
        public T Dados { get; private set; }

        public static Resposta<T> ComSucesso(T dados, IEnumerable<string> mensagens = null)
        {
            return new Resposta<T>()
            {
                Sucesso = true,
                Mensagens = mensagens,
                Dados = dados
            };
        }

        public static Resposta<T> ComSucesso(T dados, string message)
        {
            return new Resposta<T>()
            {
                Sucesso = true,
                Mensagens = ConverterMensagemLista(message),
                Dados = dados
            };
        }

        public static Resposta<T> ComFalha(T dados, IEnumerable<string> mensagens)
        {
            return new Resposta<T>()
            {
                Sucesso = false,
                Mensagens = mensagens,
                Dados = dados
            };
        }

        public static Resposta<T> ComFalha(T dados, string message)
        {
            return new Resposta<T>()
            {
                Sucesso = false,
                Mensagens = ConverterMensagemLista(message),
                Dados = dados
            };
        }

        public static new Resposta<T> ComFalha(string message)
        {
            return new Resposta<T>()
            {
                Sucesso = false,
                Mensagens = ConverterMensagemLista(message),
                Dados = default
            };
        }

        public static new Resposta<T> ComFalha(IEnumerable<string> mensagens)
        {
            return new Resposta<T>()
            {
                Sucesso = false,
                Mensagens = mensagens,
                Dados = default
            };
        }
    }
}