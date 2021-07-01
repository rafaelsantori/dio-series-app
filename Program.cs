using System;

namespace series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();

        static void Main(string[] args)
        {
            // recupera a opção selecionada pelo usuário
            string opcaoUsuario = ObterOpcaoUsuario();

            // enquanto a opção X não for selecionada pelo usuário, continua executando o programa
            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    // opção responsável por listar as séries cadastradas que não foram excluídas
                    case "1":
                        ListarSeries();
                        break;
                    // opção responsável pela inserção de uma nova série
                    case "2":
                        InserirSerie();
                        break;
                    // opção responsável pela atualização de uma série já cadastrada
                    case "3":
                        AtualizarSerie();
                        break;
                    // opção responsável pela marcação de uma série como excluída
                    case "4":
                        ExcluirSerie();
                        break;
                    // opção responsável pela exibição das informações adicionais da série
                    case "5":
                        VisualizarSerie();
                        break;
                    // opção responsável por listar as séries marcadas como excluídas
                    case "6":
                        ListarSeriesExcluidas();
                        break;
                    // opção responsável por marcar uma série como ativa
                    case "7":
                        ReativarSerie();
                        break;
                    // opção responsável por limpar o terminal
                    case "C":
                        Console.Clear();
                        break;
                    // exceção indicando que a opção escolhida não está mapeada
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                // recupera a opção selecionada pelo usuário
                opcaoUsuario = ObterOpcaoUsuario();
            }
        }

        private static void ListarSeries()
        {
            Console.Clear();
            Console.WriteLine("########### LISTAR SÉRIES ###########");

            // recupera as séries cadastradas na variável lista
            var lista = repositorio.Lista();

            int qtdSeries = 0;

            // caso não exista nenhuma série cadastrada, apresenta a mensagem
            if (lista.Count == 0)
            {
                Console.WriteLine("Não existem séries cadastradas");
                return;
            }
            else
            {
                // executa um laço de repetição para apresentar todas as séries que não estão excluídas
                foreach (var serie in lista)
                {
                    // verifica se a série foi excluída
                    var excluido = serie.RetornaExcluido();
                    if (!excluido)
                    {
                        Console.WriteLine("#ID {0}: {1}", serie.RetornaId(), serie.RetornaTitulo());
                        qtdSeries += 1;
                    }
                }
                // caso não exista nenhuma série ativa, apresenta a mensagem
                if (qtdSeries == 0)
                {
                    Console.WriteLine("Não existem séries ativas");
                    return;
                }
            }
        }

        private static void InserirSerie()
        {
            Console.Clear();
            Console.WriteLine("########### INSERIR SÉRIE ###########");

            // executa um laço de repetição para apresentar todos os gêneros cadastrados
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            // recupera os dados da série e armazena em variáveis
            Console.WriteLine("Digite o gênero entre as opções listadas: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o ano da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Console.Clear();

            // cadastra os dados informados na série
            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);
            repositorio.Insere(novaSerie);
        }

        private static void AtualizarSerie()
        {
            Console.Clear();
            Console.WriteLine("########### ATUALIZAR SÉRIE ###########");
            Console.WriteLine("Digite o ID da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            // executa um laço de repetição para apresentar todos os gêneros cadastrados
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            // recupera os dados da série e armazena em variáveis
            Console.WriteLine("Digite o gênero entre as opções listadas: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o ano da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Console.Clear();

            // cadastra os dados informados na série
            Serie atualizaSerie = new Serie(id: indiceSerie,
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            descricao: entradaDescricao,
                                            ano: entradaAno);
            repositorio.Atualiza(indiceSerie, atualizaSerie);
        }

        private static void ExcluirSerie()
        {
            Console.Clear();
            Console.WriteLine("########### EXCLUIR SÉRIE ###########");
            Console.WriteLine("Digite o ID da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            Console.Clear();

            // marca a série como excluída por meio do ID informado
            repositorio.Exclui(indiceSerie);
        }

        private static void VisualizarSerie()
        {
            Console.Clear();
            Console.WriteLine("########### VISUALIZAR SÉRIE ###########");
            Console.WriteLine("Digite o ID da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            // recupera os dados da série por meio do ID informado
            var serie = repositorio.RetornaPorId(indiceSerie);
            Console.WriteLine(serie);
        }

        private static void ListarSeriesExcluidas()
        {
            Console.Clear();
            Console.WriteLine("########### LISTAR SÉRIES EXCLUÍDAS ###########");

            // recupera as séries cadastradas na variável lista
            var lista = repositorio.Lista();

            int qtdSeries = 0;

            // caso não exista nenhuma série cadastrada, apresenta a mensagem
            if (lista.Count == 0)
            {
                Console.WriteLine("Não existem séries cadastradas");
                return;
            }
            else 
            {
                // executa um laço de repetição para apresentar as séries excluídas
                foreach (var serie in lista)
                {
                    // verifica se a série foi excluída
                    var excluido = serie.RetornaExcluido();
                    if (excluido)
                    {
                        Console.WriteLine("#ID {0}: {1}", serie.RetornaId(), serie.RetornaTitulo());
                        qtdSeries += 1;
                    }
                }
                // caso não exista nenhuma série excluída, apresenta a mensagem
                if (qtdSeries == 0)
                {
                    Console.WriteLine("Não existem séries excluídas");
                    return;
                }
            }
        }

        private static void ReativarSerie()
        {
            Console.Clear();
            Console.WriteLine("########### REATIVAR SÉRIE ###########");
            Console.WriteLine("Digite o ID da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            Console.Clear();

            // marca a série como excluída por meio do ID informado
            repositorio.Reativa(indiceSerie);
        }

        private static string ObterOpcaoUsuario()
        {
            // apresentação do menu principal do programa
            Console.WriteLine("########### MENU PRINCIPAL ###########");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("1 - Listar Séries");
            Console.WriteLine("2 - Inserir Nova Série");
            Console.WriteLine("3 - Atualizar Série");
            Console.WriteLine("4 - Excluir Série");
            Console.WriteLine("5 - Visualizar Série");
            Console.WriteLine("6 - Listar Séries Excluídas");
            Console.WriteLine("7 - Reativar Série");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();

            return opcaoUsuario;
        }
    }
}
