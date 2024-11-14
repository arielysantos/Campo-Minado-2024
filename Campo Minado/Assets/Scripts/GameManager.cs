using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Area[,] areas;  // Matriz para armazenar as áreas/células do campo minado

    [SerializeField] GameObject AreaPrefab;  // Prefab da célula (área) que será instanciada para cada célula do campo

    int diametroDoCampo;  // O tamanho (diâmetro) do campo quadrado (ex: 10x10)
    int numeroDeBombas;  // O número de bombas no campo

    bool modoBandeira;  // Modo onde o jogador pode colocar ou remover bandeiras nas células

    ManagerUI managerUI;  // Referência ao gerenciador da interface do usuário
    GameObject menu, gameOver;  // Referências para os menus de início e Game Over
    internal int Largura;  // Largura do campo (geralmente igual ao diâmetro)
    internal int Altura;  // Altura do campo (geralmente igual ao diâmetro)

    // Propriedade para acessar o modo de bandeira (usado por outras partes do código)
    public bool ModoBandeira { get => modoBandeira; }

    // Método para alternar entre o modo de bandeira
    public void AlterarModoBandeira()
    {
        modoBandeira = !modoBandeira;  // Alterna entre verdadeiro e falso
    }

    private void Start()
    {
        managerUI = GetComponent<ManagerUI>();  // Obtém o componente que gerencia a interface do usuário
        menu = GameObject.Find("Menu Window");  // Encontra o objeto "Menu Window"
        gameOver = GameObject.Find("GameOver");  // Encontra o objeto "Game Over"
    }

    // Método para definir o diâmetro do campo (tamanho da grade)
    public void DefinirDiametro(string value)
    {
        diametroDoCampo = int.Parse(value);  // Converte a string para inteiro
        managerUI.AtualizarBarra((float)numeroDeBombas / (diametroDoCampo * diametroDoCampo));  // Atualiza a barra de progresso
    }

    // Método para definir o número de bombas no campo
    public void DefinirNumeroDeBombas(string value)
    {
        numeroDeBombas = int.Parse(value);  // Converte a string para inteiro
        managerUI.AtualizarBarra((float)numeroDeBombas / (diametroDoCampo * diametroDoCampo));  // Atualiza a barra de progresso
    }

    // Método para iniciar o jogo
    public void IniciarJogo()
    {
        ExcluirCampo();  // Exclui qualquer campo existente antes de gerar um novo
        GerarCampoMinado();  // Gera o novo campo minado
        Camera.main.transform.position = new Vector3(diametroDoCampo / 2f - 0.5f, diametroDoCampo / 2f - 0.5f, -10);  // Ajusta a câmera
        Camera.main.orthographicSize = diametroDoCampo / 2f;  // Ajusta o tamanho da câmera

        DistribuirBombas();  // Distribui as bombas no campo
        menu.SetActive(false);  // Desativa o menu inicial
        gameOver.SetActive(false);  // Desativa o menu de game over
    }

    // Método para excluir o campo existente
    void ExcluirCampo()
    {
        if (areas != null)  // Verifica se o campo já existe
        {
            foreach (Area area in areas)  // Laço para percorrer todas as células do campo
            {
                Destroy(area.gameObject);  // Destroi o objeto de cada célula
            }
        }
    }

    // Método para gerar o campo minado
    public void GerarCampoMinado()
    {
        if (numeroDeBombas < Mathf.Pow(diametroDoCampo, 2))  // Verifica se o número de bombas é menor que o total de células
        {
            areas = new Area[diametroDoCampo, diametroDoCampo];  // Cria a matriz de células

            // Laço para percorrer todas as células e instanciá-las
            for (int i = 0; i < diametroDoCampo; i++)
            {
                for (int j = 0; j < diametroDoCampo; j++)
                {
                    Area area = Instantiate(AreaPrefab, new Vector2(i, j), Quaternion.identity).GetComponent<Area>();  // Cria a célula
                    area.DefinirIndex(i, j);  // Define o índice da célula
                    areas[i, j] = area;  // Adiciona a célula à matriz
                }
            }
        }
    }

    // Método para checar o entorno de uma célula e contar as bombas vizinhas
    public int ChecarEntorno(int x, int y)
    {
        int quantidadeDeBombas = 0;  // Variável para contar o número de bombas vizinhas

        // Laços para percorrer as 8 células vizinhas (ao redor de x, y)
        for (int i = -1; i < 2; i++)  // Laço para as linhas vizinhas (-1, 0, +1)
        {
            for (int j = -1; j < 2; j++)  // Laço para as colunas vizinhas (-1, 0, +1)
            {
                // Verifica se a célula vizinha está dentro dos limites do campo
                if (x + i < diametroDoCampo && y + j < diametroDoCampo && x + i >= 0 && y + j >= 0)
                {
                    // Se a célula vizinha tiver uma bomba, aumenta o contador
                    if (areas[x + i, y + j].Bomba)
                    {
                        quantidadeDeBombas++;
                    }
                }
            }
        }

        // Se não houver bombas ao redor (quantidadeDeBombas == 0), revela as células vizinhas
        if (quantidadeDeBombas == 0)
        {
            for (int i = -1; i < 2; i++)  // Laço para percorrer as células vizinhas
            {
                for (int j = -1; j < 2; j++)  // Laço para percorrer as células vizinhas
                {
                    // Verifica se a célula vizinha está dentro dos limites do campo
                    if (x + i < diametroDoCampo && y + j < diametroDoCampo && x + i >= 0 && y + j >= 0)
                    {
                        areas[x + i, y + j].Clicado();  // Chama o método Clicado() nas células vizinhas
                    }
                }
            }
        }

        return quantidadeDeBombas;  // Retorna o número de bombas vizinhas
    }
}
