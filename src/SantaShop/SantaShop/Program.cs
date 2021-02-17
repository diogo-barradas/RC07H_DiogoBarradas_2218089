
using System;
using Dapper;
using Dapper.Contrib;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using System.Linq;

namespace SantaSHOP
{
    [Table("criancas")]
    class crianca
    {
        [Key]
        public int crianca_id { get; set; }
        public int idade { get; set; }
        public string nome { get; set; }

    }

    [Table("presentes")]
    class presentes
    {
        [Key]
        public int presenteid { get; set; }
        public int quantidade { get; set; }
        public string nome { get; set; }

    }

    [Table("comportamento")]
    class comportamento
    {
        [Key]
        public int comportamento_id { get; set; }
        public string descricao { get; set; }
        public bool condicao { get; set; }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection ...");

            //var datasource = "localhost"; //your server
            //var database = "santashop"; //your database name
            //var username = "root"; //username of server to connect
            //var password = ""; //password

            //your connection string 
            string connString = @"Data Source=localhost;Initial Catalog=santashop;Persist Security Info=True;User ID=root;Password=";

            //create instanace of database connection
            MySqlConnection connection = new MySqlConnection(connString);

            try
            {
                Console.WriteLine("Openning Connection ...");

                //open connection
                connection.Open();

                Console.WriteLine("Connection successful!");
                Console.ReadKey();

            Start:
                int OpcaoMenu = 0;
                int OpcaoMenuPresente = 0;
                int OpcaoMenuCiranca = 0;
                int OpcaoComportamento = 0;

                Console.Clear();
                Console.WriteLine("-/-/-/ MENU /-/-/- \n 1- Criancas\n 2- Presentes\n 3- Comportamento\n 4- Voltar");
                OpcaoMenu = Convert.ToInt32(Console.ReadLine());

                switch (OpcaoMenu)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("-/-/-/ Crianca /-/-/- \n 1- Mostrar Cirancas\n 2- Criar Criancas\n 3- MATAR Criancas\n 4- Voltar");
                        OpcaoMenuCiranca = Convert.ToInt32(Console.ReadLine());

                        switch (OpcaoMenuCiranca)
                        {
                            case 1:
                                try
                                {
                                    var criancaLista = connection.GetAll<crianca>().ToList();
                                    criancaLista.ForEach(i => Console.Write("\nID: " + i.crianca_id + "\nnome: " + i.nome + "\nIdade: " + i.idade + "\n ---"));

                                    Console.WriteLine("\n\nLista Completa\n Carrega ENTER para continuar ...");
                                    Console.ReadKey();

                                    goto Start;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error: " + e.Message);
                                }
                                break;
                            case 2:
                                Console.WriteLine("Qual o nome da crianca:");
                                string nomeCrianca = Convert.ToString(Console.ReadLine());

                                Console.WriteLine("Qual a idade do " + nomeCrianca + "?");
                                int idadeCrianca = Convert.ToInt32(Console.ReadLine());

                                try
                                {
                                    var addCrianca = new crianca
                                    {
                                        idade = idadeCrianca,
                                        nome = nomeCrianca
                                    };

                                    connection.Insert<crianca>(addCrianca);
                                    Console.WriteLine("\nBOAA!! " + nomeCrianca + "foi adicionado\n\n\n Clica numa tecla para contiunuar");
                                    Console.ReadKey();

                                    goto Start;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error: " + e.Message);
                                }
                                break;
                            case 3:
                                var criancaListaM = connection.GetAll<crianca>().ToList();
                                criancaListaM.ForEach(i => Console.Write("\nID: " + i.crianca_id + "\nNome: " + i.nome + "\nIdade: " + i.idade + "\n ---"));

                                Console.WriteLine("\n\n Digite o ID da crianca que pretende homicidar: ");
                                int idMatar = Convert.ToInt32(Console.ReadLine());

                                connection.Delete(new crianca { crianca_id = idMatar });
                                Console.WriteLine("\nO seu hitman pessoas conclui o objetivo! BOA\n\n Clique numa tecla para continuar...");
                                Console.ReadKey();

                                goto Start;
                                break;
                            case 4:
                                goto Start;
                                break;
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("-/-/-/ Presente /-/-/- \n 1- Dar Presentes\n 2- Criar Presente \n 3- Remover Presente \n 4 - Voltar ");
                        OpcaoMenuPresente = Convert.ToInt32(Console.ReadLine());

                        switch (OpcaoMenuPresente)
                        {
                            case 1:
                                try
                                {
                                    var criancaLista = connection.GetAll<presentes>().ToList();
                                    criancaLista.ForEach(i => Console.Write("\n ID: " + i.presenteid + "\n Nome: " + i.nome + "\n Quantidade: " + i.quantidade));

                                    Console.WriteLine("\nLista Completa\n Carrega ENTER para continuar ...");
                                    Console.ReadKey();

                                    goto Start;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error: " + e.Message);
                                }
                                break;
                            case 2:
                                Console.WriteLine("Qual o nome do presente");
                                string nome = Convert.ToString(Console.ReadLine());

                                Console.WriteLine("Qual a quantidade de " + nome + "?");
                                int quantidade = Convert.ToInt32(Console.ReadLine());

                                try
                                {
                                    var addPresente = new presentes
                                    {
                                        quantidade = quantidade,
                                        nome = nome
                                    };

                                    connection.Insert(addPresente);

                                    Console.WriteLine("\n BOAA!! " + nome + " foi adicionado\n Carrega ENTER para continuar ...");
                                    Console.ReadKey();

                                    goto Start;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error: " + e.Message);
                                }
                                break;
                            case 3:
                                var presenteListaM = connection.GetAll<presentes>().ToList();
                                presenteListaM.ForEach(i => Console.Write("\n ID: " + i.presenteid + "\n Nome: " + i.nome + "\n Quantidade: " + i.quantidade));
                                Console.WriteLine("\n\n Digite o ID do presente que pretende eliminar: ");

                                int idMatara = Convert.ToInt32(Console.ReadLine());
                                connection.Delete(new presentes { presenteid = idMatara });
                                Console.WriteLine("\n Eliminou um presente!\n Carrega ENTER para continuar ...");

                                Console.ReadKey();
                                goto Start;
                                break;
                            case 4:
                                goto Start;
                                break;
                        }
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("-/-/-/ Comportamento /-/-/- \n 1- Dar Comportamento\n 2- Remover Comportamento\n 3 - Voltar");
                        OpcaoMenuPresente = Convert.ToInt32(Console.ReadLine());

                        switch (OpcaoComportamento)
                        {
                            case 1:
                                Console.WriteLine("Qual o id da crianca:");
                                int idcriancaComportamento = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Definir descricao:");
                                string descricao = Convert.ToString(Console.ReadLine());

                                Console.WriteLine("Definir condicao: \n 1- Receber\n 2- Nao Recebe");
                                int condicaoInt = Convert.ToInt32(Console.ReadLine());

                                try
                                {
                                    bool condicao = false;
                                    if (condicaoInt == 1)
                                    {
                                        condicao = true;
                                    }
                                    else if (condicaoInt == 2)
                                    {
                                        condicao = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERRO");
                                    }

                                    var addComportamento = new comportamento
                                    {
                                        descricao = descricao,
                                        condicao = condicao
                                    };

                                    connection.Insert(addComportamento);

                                    Console.WriteLine("\n BOAA!! " + addComportamento + " foi adicionado\n Carrega ENTER para continuar ...");
                                    Console.ReadKey();

                                    goto Start;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error: " + e.Message);
                                }
                                break;
                            case 2:
                                var presenteListaComp = connection.GetAll<comportamento>().ToList();
                                presenteListaComp.ForEach(i => Console.Write("\n ID: " + i.comportamento_id + "\n Descricao: " + i.descricao + "\n Condicao: " + i.condicao));
                                Console.WriteLine("\n\n Digite o ID da crianca que pretende eliminar: ");

                                int idMatara = Convert.ToInt32(Console.ReadLine());
                                connection.Delete(new presentes { presenteid = idMatara });
                                Console.WriteLine("\n Eliminou uma pessoa!\n Carrega ENTER para continuar ...");

                                Console.ReadKey();
                                goto Start;
                                break;
                            case 3:
                                goto Start;
                                break;
                        }
                        goto Start;
                        break;
                    case 4:
                        goto Start;
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.Read();
        }
    }
}