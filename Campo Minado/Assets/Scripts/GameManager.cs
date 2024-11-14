using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    // Singleton para garantir que o GameManager seja acessado de qualquer parte do código
    public static GameManager instance;

    private void Awake()
    {
        instance = this; // Atribui a instância do GameManager
    }
    #endregion

    Area[,] areas; // Matriz que contém as áreas do campo minado, cada célula é um objeto 'Area'

    [SerializeField] GameObject AreaPrefab; // Prefab da área (a célula do campo minado)

    int diametroDoCampo; // O tamanho da grade quadrada do campo
    int numeroDeBombas; // Número total de bombas no campo

    bool modoBandeira; // Flag para verificar se o jogador está no modo de bandeira (marcando ou desmarcando bombas)

    ManagerUI managerUI; // Referência ao script que gerencia a interface de usuário
    GameObject menu, gameOver; // Referências para os objetos de Menu e Game Over

    // Propriedade para acessar o estado do modo bandeira
    public bool ModoBandeira { get => modoBandeira; }

    // Alterna entre os modos de bandeira (ativa ou desativa)
    public void AlterarModoBandeira()
    {
        modoBandeira = !modoBandeira; // Inverte o estado do modo bandeira
    }

    // Método de inicialização, configurando a interface e preparando os objetos necessários
    private void Start()
    {
        managerUI = GetComponent<ManagerUI>(); // Obtém a referência ao script de gerenciamento da interface
        menu = GameObject.Find("Menu Window"); // Encontra o objeto Menu na cena
        gameOver = GameObject.Find("GameOver"); // Encontra o objeto Game Over na cena
    }

    // Define o tamanho do campo (número de células por lado) e atualiza a barra de progresso com o número de bombas
    public void DefinirDiametro(string value)
    {
        diametroDoCampo = int.Parse(value); // Converte o valor para inteiro
        managerUI.AtualizarBarra((float)numeroDeBombas / (diametroDoCampo * diametroDoCampo)); // Atualiza a barra de progresso com base no número de bombas
    }

    // Define o número de bombas no campo e atualiza a barra de progresso
    public void DefinirNumeroDeBombas(string value)
    {
        numeroDeBombas = int.Parse(value); // Converte o valor para inteiro
        managerUI.AtualizarBarra((float)numeroDeBombas / (diametroDoCampo * diametroDoCampo)); // Atualiza a barra de progresso com base no número de bombas
    }

    // Inicia o jogo, gera o campo minado, coloca as bombas e ajusta a câmera
    public void IniciarJogo()
    {
        ExcluirCampo(); // Exclui o campo anterior, se houver
        GerarCampoMinado(); // Gera o novo campo minado
        Camera.main.transform.position = new Vector3(diametroDoCampo / 2f - 0.5f, diametroDoCampo / 2f - 0.5f, -10); // Ajusta a posição da câmera
        Camera.main.orthographicSize = diametroDoCampo / 2f; // Ajusta o tamanho da câmera com base no tamanho do campo

        DistribuirBombas(); // Coloca as bombas no campo
        menu.SetActive(false); // Desativa o menu
        gameOver.SetActive(false); // Desativa a tela de Game Over
    }

    private void DistribuirBombas()
    {
        throw new NotImplementedException();
    }

    // Exclui o campo minado atual (se já existir) antes de criar um novo
    void ExcluirCampo()
    {
        if (areas != null) // Verifica se a matriz de áreas já foi criada
        {
            foreach (Area area in areas)
            {
                Destroy(area.gameObject); // Destroi todas as áreas existentes
            }
        }
    }

    // Gera o campo minado (cria uma nova matriz de áreas)
    public void GerarCampoMinado()
    {
        if (numeroDeBombas < Mathf.Pow(diametroDoCampo, 2)) // Verifica se o número de bombas não é maior que o número de células
        {
            areas = new Area[diametroDoCampo, diametroDoCampo]; // Cria uma nova matriz para o campo

            // Cria cada área (célula) no campo
            for (int i = 0; i < diametroDoCampo; i++)
            {
                for (int j = 0; j < diametroDoCampo; j++)
                {
                    Area area = Instantiate(AreaPrefab, new Vector2(i, j), Quaternion.identity).GetComponent<Area>(); // Instancia o prefab da área
                    area.DefinirIndex(i, j); // Define a posição da área na matriz
                    areas[i, j] = area; // Adiciona a área na matriz
                }
            }
        }
    }

    // Método para verificar o número de bombas ao redor de uma posição (x, y)
    public int ChecarEntorno(int x, int y)
    {
        int quantidadeDeBombas = 0; // Inicializa a contagem de bombas ao redor


        
    }

    internal void GameOver()
    {
        throw new NotImplementedException();
    }

    internal void ChecarVitoria()
    {
        throw new NotImplementedException();
    }
}



