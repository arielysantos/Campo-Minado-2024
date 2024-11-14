using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Area[,] areas;  // Matriz para armazenar as �reas/c�lulas do campo minado

    [SerializeField] GameObject AreaPrefab;  // Prefab da c�lula (�rea) que ser� instanciada para cada c�lula do campo

    int diametroDoCampo;  // O tamanho (di�metro) do campo quadrado (ex: 10x10)
    int numeroDeBombas;  // O n�mero de bombas no campo

    bool modoBandeira;  // Modo onde o jogador pode colocar ou remover bandeiras nas c�lulas

    ManagerUI managerUI;  // Refer�ncia ao gerenciador da interface do usu�rio
    GameObject menu, gameOver;  // Refer�ncias para os menus de in�cio e Game Over
    internal int Largura;  // Largura do campo (geralmente igual ao di�metro)
    internal int Altura;  // Altura do campo (geralmente igual ao di�metro)

    // Propriedade para acessar o modo de bandeira (usado por outras partes do c�digo)
    public bool ModoBandeira { get => modoBandeira; }

    // M�todo para alternar entre o modo de bandeira
    public void AlterarModoBandeira()
    {
        modoBandeira = !modoBandeira;  // Alterna entre verdadeiro e falso
    }

    private void Start()
    {
        managerUI = GetComponent<ManagerUI>();  // Obt�m o componente que gerencia a interface do usu�rio
        menu = GameObject.Find("Menu Window");  // Encontra o objeto "Menu Window"
        gameOver = GameObject.Find("GameOver");  // Encontra o objeto "Game Over"
    }

    // M�todo para definir o di�metro do campo (tamanho da grade)
    public void DefinirDiametro(string value)
    {
        diametroDoCampo = int.Parse(value);  // Converte a string para inteiro
        managerUI.AtualizarBarra((float)numeroDeBombas / (diametroDoCampo * diametroDoCampo));  // Atualiza a barra de progresso
    }

    // M�todo para definir o n�mero de bombas no campo
    public void DefinirNumeroDeBombas(string value)
    {
        numeroDeBombas = int.Parse(value);  // Converte a string para inteiro
        managerUI.AtualizarBarra((float)numeroDeBombas / (diametroDoCampo * diametroDoCampo));  // Atualiza a barra de progresso
    }

    // M�todo para iniciar o jogo
    public void IniciarJogo()
    {
        ExcluirCampo();  // Exclui qualquer campo existente antes de gerar um novo
        GerarCampoMinado();  // Gera o novo campo minado
        Camera.main.transform.position = new Vector3(diametroDoCampo / 2f - 0.5f, diametroDoCampo / 2f - 0.5f, -10);  // Ajusta a c�mera
        Camera.main.orthographicSize = diametroDoCampo / 2f;  // Ajusta o tamanho da c�mera

        DistribuirBombas();  // Distribui as bombas no campo
        menu.SetActive(false);  // Desativa o menu inicial
        gameOver.SetActive(false);  // Desativa o menu de game over
    }

    // M�todo para excluir o campo existente
    void ExcluirCampo()
    {
        if (areas != null)  // Verifica se o campo j� existe
        {
            foreach (Area area in areas)  // La�o para percorrer todas as c�lulas do campo
            {
                Destroy(area.gameObject);  // Destroi o objeto de cada c�lula
            }
        }
    }

    // M�todo para gerar o campo minado
    public void GerarCampoMinado()
    {
        if (numeroDeBombas < Mathf.Pow(diametroDoCampo, 2))  // Verifica se o n�mero de bombas � menor que o total de c�lulas
        {
            areas = new Area[diametroDoCampo, diametroDoCampo];  // Cria a matriz de c�lulas

            // La�o para percorrer todas as c�lulas e instanci�-las
            for (int i = 0; i < diametroDoCampo; i++)
            {
                for (int j = 0; j < diametroDoCampo; j++)
                {
                    Area area = Instantiate(AreaPrefab, new Vector2(i, j), Quaternion.identity).GetComponent<Area>();  // Cria a c�lula
                    area.DefinirIndex(i, j);  // Define o �ndice da c�lula
                    areas[i, j] = area;  // Adiciona a c�lula � matriz
                }
            }
        }
    }

    // M�todo para checar o entorno de uma c�lula e contar as bombas vizinhas
    public int ChecarEntorno(int x, int y)
    {
        int quantidadeDeBombas = 0;  // Vari�vel para contar o n�mero de bombas vizinhas

        // La�os para percorrer as 8 c�lulas vizinhas (ao redor de x, y)
        for (int i = -1; i < 2; i++)  // La�o para as linhas vizinhas (-1, 0, +1)
        {
            for (int j = -1; j < 2; j++)  // La�o para as colunas vizinhas (-1, 0, +1)
            {
                // Verifica se a c�lula vizinha est� dentro dos limites do campo
                if (x + i < diametroDoCampo && y + j < diametroDoCampo && x + i >= 0 && y + j >= 0)
                {
                    // Se a c�lula vizinha tiver uma bomba, aumenta o contador
                    if (areas[x + i, y + j].Bomba)
                    {
                        quantidadeDeBombas++;
                    }
                }
            }
        }

        // Se n�o houver bombas ao redor (quantidadeDeBombas == 0), revela as c�lulas vizinhas
        if (quantidadeDeBombas == 0)
        {
            for (int i = -1; i < 2; i++)  // La�o para percorrer as c�lulas vizinhas
            {
                for (int j = -1; j < 2; j++)  // La�o para percorrer as c�lulas vizinhas
                {
                    // Verifica se a c�lula vizinha est� dentro dos limites do campo
                    if (x + i < diametroDoCampo && y + j < diametroDoCampo && x + i >= 0 && y + j >= 0)
                    {
                        areas[x + i, y + j].Clicado();  // Chama o m�todo Clicado() nas c�lulas vizinhas
                    }
                }
            }
        }

        return quantidadeDeBombas;  // Retorna o n�mero de bombas vizinhas
    }
}
