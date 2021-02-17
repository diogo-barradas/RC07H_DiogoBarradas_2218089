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

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection ...");

            var datasource = "localhost";//your server
            var database = "santashop"; //your database name
            var username = "root"; //username of server to connect
            var password = ""; //password

            //your connection string 
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            //create instanace of database connection
            MySqlConnection conn = new MySqlConnection(connString);


            try
            {
                Console.WriteLine("Openning Connection ...");

                //open connection
                conn.Open();

                Console.WriteLine("Connection successful!");
                Console.ReadKey();

                Start:

                int OpcaoMenu = 0;
                int OpcaoMenuCiranca = 0;
                int OpcaoMenuPresente = 0;
                Console.Clear();
                Console.WriteLine("santaSHOP");
                Console.WriteLine("Clique numa tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("-/-/-/ MENU /-/-/- \n 1- Criancas\n 2- Presentes\n 3- Comportamento \n4- Voltar");
                OpcaoMenu = Convert.ToInt32(Console.ReadLine());
                switch (OpcaoMenu)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("-/-/-/ Crianca /-/-/- \n 1- Mostrar Cirancas\n 2- Criar Criancas \n 3- MATAR Criancas \n 4 - Voltar ");
                        OpcaoMenuCiranca = Convert.ToInt32(Console.ReadLine());
                        switch (OpcaoMenuCiranca)
                        {
                            case 1:
                                try
                                {
                                    var criancaLista = conn.GetAll<crianca>().ToList();
                                    criancaLista.ForEach(i => Console.Write("\nID: " + i.crianca_id + "\nnome: " + i.nome + "\nIdade: " + i.idade + "\n ---"));
                                    Console.WriteLine("\n\nLista Complete\n Digite uma tecla para continuar");
                                    Console.ReadKey();
                                    goto Start;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error: " + e.Message);
                                }
                                break;

                             case 2:
                                string nomeCrianca;
                                int idadeCrianca;
                                Console.WriteLine("Qual o nome da crianca");
                                nomeCrianca = Convert.ToString(Console.ReadLine());
                                Console.WriteLine("Qual a idade do " + nomeCrianca + " ?");
                                idadeCrianca = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                    var addCrianca = new crianca
                                    {
                                        idade = idadeCrianca,
                                        nome = nomeCrianca
                                    };
                                    conn.Insert<crianca>(addCrianca);
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
                                var criancaListaM = conn.GetAll<crianca>().ToList();
                                criancaListaM.ForEach(i => Console.Write("\nID: " + i.crianca_id + "\nNome: " + i.nome + "\nIdade: " + i.idade + "\n ---"));
                                Console.WriteLine("\n\n Digite o ID da crianca que pretende homicidar: ");
                                int idMatar = Convert.ToInt32(Console.ReadLine());
                                conn.Delete(new crianca { crianca_id = idMatar });
                                Console.WriteLine("\nO seu hitman pessoas conclui o objetivo! BOA\n\n Clique numa tecla para continuar...");
                                Console.ReadKey();
                                goto Start;
                                break;
                            default:
                                Console.WriteLine("\n\n\n\nDigite valores entre 1 e 4");
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
                                    var criancaLista = conn.GetAll<presentes>().ToList();
                                    criancaLista.ForEach(i => Console.Write("\nID: " + i.presenteid + "\nNome: " + i.nome + "\nIdade: " + i.quantidade + "\n ---"));
                                    Console.WriteLine("\n\nLista Complete\n Digite uma tecla para continuar");
                                    Console.ReadKey();
                                    goto Start;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error: " + e.Message);
                                }
                                break;
                            case 2:
                                string nome;
                                int quantidade;
                                Console.WriteLine("Qual o nome do presente");
                                nome = Convert.ToString(Console.ReadLine());
                                Console.WriteLine("Qual a quantidade de " + nome + " ?");
                                quantidade = Convert.ToInt32(Console.ReadLine());

                                try
                                {
                                    var addPresente = new crianca
                                    {
                                        idade = quantidade,
                                        nome = nome
                                    };
                                    conn.Insert(addPresente);
                                    Console.WriteLine("\nBOAA!! " + nome + "foi adicionado\n\n\n Clica numa tecla para contiunuar");
                                    Console.ReadKey();
                                    goto Start;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error: " + e.Message);
                                }
                                break;
                            case 3:
                                var presenteListaM = conn.GetAll<presentes>().ToList();
                                presenteListaM.ForEach(i => Console.Write("\nID: " + i.presenteid + "\nNome: " + i.nome + "\nIdade: " + i.quantidade + "\n ---"));
                                Console.WriteLine("\n\n Digite o ID da crianca que pretende homicidar: ");
                                int idMatara = Convert.ToInt32(Console.ReadLine());
                                conn.Delete(new presentes { presenteid = idMatara });
                                Console.WriteLine("\nO seu hitman pessoas conclui o objetivo! BOA\n\n Clique numa tecla para continuar...");
                                Console.ReadKey();
                                goto Start;
                                break;
                            case 4:
                                goto Start;
                                break;
                            default:
                                Console.WriteLine("\n\n\n\nDigite valores entre 1 e 4");
                                break;
                        }
                        break;
                    case 3:
                        break;
                    case 4:
                        goto Start;
                        break;
                    default: Console.WriteLine("\n\n\n\nDigite valores entre 1 e 4");
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