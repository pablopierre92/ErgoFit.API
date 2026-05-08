using Ergo.Fit.DataContext;
using Ergo.Fit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ergo.Fit.Configuration
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            await SeedMasterUserAsync(userManager, context, config);
            await SeedCategoriasAsync(context);
            await SeedExerciciosAsync(context);
        }

        private static async Task SeedMasterUserAsync(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IConfiguration config)
        {
            var masterEmail = config["MasterSeed:Email"] ?? "admin@ergofit.com";
            var masterSenha = config["MasterSeed:Senha"] ?? "ErgoFit@2024";
            var masterNome = config["MasterSeed:Nome"] ?? "Administrador ErgoFit";

            if (await userManager.FindByEmailAsync(masterEmail) != null) return;

            var user = new ApplicationUser
            {
                UserName = masterEmail,
                Email = masterEmail,
                NomeCompleto = masterNome,
                EmailConfirmed = true,
                Ativo = true
            };

            var result = await userManager.CreateAsync(user, masterSenha);
            if (!result.Succeeded) return;

            await userManager.AddToRoleAsync(user, "UsuarioMaster");

            context.UsuariosMaster.Add(new UsuarioMasterModel
            {
                Nome = masterNome,
                Email = masterEmail,
                ApplicationUserId = user.Id,
                Ativo = true
            });
            await context.SaveChangesAsync();
        }

        private static async Task SeedCategoriasAsync(ApplicationDbContext context)
        {
            if (await context.Categorias.AnyAsync()) return;

            var categorias = new[]
            {
                new CategoriaModel { Nome = "Ombro", Descricao = "Exercícios para ombros e região escapular", Ativo = true },
                new CategoriaModel { Nome = "Pescoço", Descricao = "Exercícios para cervical e pescoço", Ativo = true },
                new CategoriaModel { Nome = "Coluna", Descricao = "Exercícios para coluna lombar e dorsal", Ativo = true },
                new CategoriaModel { Nome = "Punho", Descricao = "Exercícios para punhos e antebraço", Ativo = true },
                new CategoriaModel { Nome = "Polegar", Descricao = "Exercícios para polegar e articulações do dedo", Ativo = true },
                new CategoriaModel { Nome = "Mão", Descricao = "Exercícios para mãos e dedos", Ativo = true },
                new CategoriaModel { Nome = "Joelho", Descricao = "Exercícios para joelhos e região patelar", Ativo = true },
                new CategoriaModel { Nome = "Pé", Descricao = "Exercícios para pés e tornozelo", Ativo = true },
                new CategoriaModel { Nome = "Paturrilha", Descricao = "Exercícios para panturrilha e gastrocnêmio", Ativo = true },
            };

            context.Categorias.AddRange(categorias);
            await context.SaveChangesAsync();
        }

        private static async Task SeedExerciciosAsync(ApplicationDbContext context)
        {
            if (await context.Exercicios.AnyAsync()) return;

            var cats = await context.Categorias.ToDictionaryAsync(c => c.Nome, c => c.Id);

            var exercicios = new List<ExercicioModel>
            {
                // Ombro
                new() { Nome = "Rotação de Ombros", Descricao = "Movimentos circulares dos ombros para relaxar a musculatura.", Instrucoes = "Sente-se ou fique em pé com a coluna ereta. Faça círculos lentos com os ombros, 10x para frente e 10x para trás.", DuracaoEstimada = 60, IdCategoria = cats["Ombro"], Ativo = true },
                new() { Nome = "Elevação Lateral de Braços", Descricao = "Eleva os braços lateralmente para alongar o deltóide.", Instrucoes = "Mantenha os braços estendidos e eleve lentamente até a altura do ombro. Segure 5 segundos e abaixe. Repita 10x.", DuracaoEstimada = 90, IdCategoria = cats["Ombro"], Ativo = true },

                // Pescoço
                new() { Nome = "Flexão Lateral do Pescoço", Descricao = "Inclina a cabeça para cada lado aliviando a tensão cervical.", Instrucoes = "Incline a cabeça para o lado direito, aproximando a orelha do ombro. Segure 15s. Repita para o lado esquerdo.", DuracaoEstimada = 60, IdCategoria = cats["Pescoço"], Ativo = true },
                new() { Nome = "Rotação Cervical", Descricao = "Gira o pescoço lentamente para mobilizar a cervical.", Instrucoes = "Gire a cabeça lentamente para a direita, segure 10s. Volte ao centro e repita para a esquerda. Faça 5 repetições por lado.", DuracaoEstimada = 90, IdCategoria = cats["Pescoço"], Ativo = true },

                // Coluna
                new() { Nome = "Alongamento de Coluna em Cadeira", Descricao = "Flexão da coluna sentado para aliviar a lombar.", Instrucoes = "Sentado, incline o tronco para frente até que o peito toque as coxas. Relaxe e segure 20s. Repita 3x.", DuracaoEstimada = 90, IdCategoria = cats["Coluna"], Ativo = true },
                new() { Nome = "Torção de Tronco", Descricao = "Rotação do tronco para mobilizar a coluna torácica.", Instrucoes = "Sentado com os pés no chão, gire o tronco para a direita segurando 15s. Repita para a esquerda. 5 repetições por lado.", DuracaoEstimada = 120, IdCategoria = cats["Coluna"], Ativo = true },

                // Punho
                new() { Nome = "Flexão e Extensão de Punho", Descricao = "Mobiliza as articulações do punho prevenindo LER.", Instrucoes = "Com o braço estendido, dobre o punho para baixo com a ajuda da outra mão. Segure 15s. Depois dobre para cima. Repita 5x.", DuracaoEstimada = 60, IdCategoria = cats["Punho"], Ativo = true },
                new() { Nome = "Círculos com Punho", Descricao = "Movimentos circulares para mobilizar o punho.", Instrucoes = "Com os punhos fechados, faça círculos lentos no sentido horário e anti-horário. 10 círculos para cada lado.", DuracaoEstimada = 60, IdCategoria = cats["Punho"], Ativo = true },

                // Polegar
                new() { Nome = "Oposição do Polegar", Descricao = "Exercício de coordenação e força do polegar.", Instrucoes = "Toque a ponta do polegar em cada um dos dedos, do indicador ao mínimo. Repita 5x em cada mão.", DuracaoEstimada = 45, IdCategoria = cats["Polegar"], Ativo = true },

                // Mão
                new() { Nome = "Abrir e Fechar as Mãos", Descricao = "Ativa a circulação e relaxa os músculos das mãos.", Instrucoes = "Abra as mãos com os dedos bem estendidos. Feche em punho firme. Repita 20 vezes em ritmo moderado.", DuracaoEstimada = 45, IdCategoria = cats["Mão"], Ativo = true },
                new() { Nome = "Entrelace os Dedos", Descricao = "Alonga os dedos e o dorso das mãos.", Instrucoes = "Entrelace os dedos das duas mãos e vire as palmas para fora. Estenda os braços à frente. Segure 20s. Repita 3x.", DuracaoEstimada = 60, IdCategoria = cats["Mão"], Ativo = true },

                // Joelho
                new() { Nome = "Extensão de Joelho Sentado", Descricao = "Fortalece o quadríceps e mobiliza o joelho.", Instrucoes = "Sentado, estenda uma perna até ficar reta e segure 10s. Abaixe lentamente. 10 repetições por perna.", DuracaoEstimada = 90, IdCategoria = cats["Joelho"], Ativo = true },
                new() { Nome = "Agachamento Parcial", Descricao = "Fortalece joelhos e glúteos com amplitude reduzida.", Instrucoes = "De pé, desça em agachamento parcial (45°) e segure 5s. Retorne. Faça 10 repetições.", DuracaoEstimada = 60, IdCategoria = cats["Joelho"], Ativo = true },

                // Pé
                new() { Nome = "Rotação de Tornozelo", Descricao = "Mobiliza o tornozelo e ativa a circulação dos pés.", Instrucoes = "Sentado, levante um pé e faça círculos com o tornozelo. 10x no sentido horário e 10x anti-horário. Repita com o outro pé.", DuracaoEstimada = 60, IdCategoria = cats["Pé"], Ativo = true },
                new() { Nome = "Elevação na Ponta dos Pés", Descricao = "Fortalece o tornozelo e melhora o equilíbrio.", Instrucoes = "De pé (pode se apoiar), suba na ponta dos pés e segure 3s. Desça lentamente. Repita 15 vezes.", DuracaoEstimada = 60, IdCategoria = cats["Pé"], Ativo = true },

                // Paturrilha
                new() { Nome = "Alongamento de Panturrilha", Descricao = "Alonga o gastrocnêmio e o sóleo.", Instrucoes = "De pé, dê um passo à frente e apoie as mãos na parede. Mantenha o calcanhar traseiro no chão e avance o quadril. Segure 30s por perna.", DuracaoEstimada = 90, IdCategoria = cats["Paturrilha"], Ativo = true },
            };

            context.Exercicios.AddRange(exercicios);
            await context.SaveChangesAsync();
        }
    }
}
