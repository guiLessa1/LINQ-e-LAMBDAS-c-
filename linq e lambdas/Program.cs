using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq_e_lambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Funcionario> funcionarios = new List<Funcionario>
            {
                new Funcionario("Ana", "RH", 3500, 5),
                new Funcionario("Bruno", "TI", 4000, 12),
                new Funcionario("Carlos", "Financeiro", 2800, 8),
                new Funcionario("Daniela", "TI", 4500, 15),
                new Funcionario("Eduardo", "RH", 3200, 11),
                new Funcionario("Fernanda", "Marketing", 2900, 3)
            };

            //Funcionários com salário > 3000
            var salarioMaiorQue3000 = funcionarios.Where(f => f.Salario > 3000).ToList();

            //5% de aumento para quem tem > 10 anos de serviço
            funcionarios.Where(f => f.AnosDeServico > 10).ToList().ForEach(f => f.Salario *= 1.05);

            //Ordenar por nome
            var ordenadosPorNome = funcionarios.OrderBy(f => f.Nome).ToList();

            //Calcular total gasto com salários
            double totalSalarios = funcionarios.Sum(f => f.Salario);

            //Agrupar por departamento e mostrar media salarial
            var mediaPorDepartamento = funcionarios
                .GroupBy(f => f.Departamento)
                .Select(g => new
            {
                Departamento = g.Key,
                MediaSalarial = g.Average(f => f.Salario)
            });

            //Funcionário mais antigo
            var maisAntigo = funcionarios.OrderByDescending(f => f.AnosDeServico).FirstOrDefault();

            Action<Funcionario> imprimirFuncionario = f =>
            Console.WriteLine($"Nome: {f.Nome} | Departamento: {f.Departamento} | Salario: {f.Salario} | Anos de Serviço: {f.AnosDeServico}");

            Console.WriteLine("=== Funcionários com salário maior que 3000 ===");
            salarioMaiorQue3000.ForEach(imprimirFuncionario);

            Console.WriteLine("\n=== Funcionários ordenados por Nome ===");
            ordenadosPorNome.ForEach(imprimirFuncionario);

            Console.WriteLine($"\n=== Total gasto com salários: {totalSalarios:C2} ===");

            Console.WriteLine("\n=== Média salario por departamento ===");
            foreach (var grupo in mediaPorDepartamento)
            {
                Console.WriteLine($"Departamento: {grupo.Departamento} | Média Salarial: {grupo.MediaSalarial:C2}");
            }

            Console.WriteLine("\n === Funcionários com mais tempo de serviço ===");
            if (maisAntigo != null)
            {
                imprimirFuncionario(maisAntigo);
            }

            Console.ReadKey();
        }
    }

    public class Funcionario
    {
        public string Nome { get; set; }
        public string Departamento { get; set; }
        public double Salario { get; set; }
        public int AnosDeServico { get; set; }

        public Funcionario(string nome, string departamento, double salario, int anosDeServico)
        {
            Nome = nome;
            Departamento = departamento;
            Salario = salario;
            AnosDeServico = anosDeServico;
        }
    }
}
