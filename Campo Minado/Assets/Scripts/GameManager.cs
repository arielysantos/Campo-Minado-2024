using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    // Singleton para garantir que o GameManager seja acessado de qualquer parte do c�digo
    public static GameManager instance;

    private void Awake()
    {
        instance = this; // Atribui a inst�ncia do GameManager
    }
    #endregion

    Area[,] areas; // Matriz que cont�m as �reas do campo minado, cada c�lula � um objeto 'Area'

    [SerializeField] GameObject AreaPrefab; // Prefab da �rea (a c�lula do campo minado)

    int diametroDoCampo; // O tamanho da grade quadrada do campo
    int numeroDeBombas; // N�mero total de bombas no campo

    bool modoBandeira; // Flag para verificar se o jogador est� no modo de bandeira (marcando ou desmarcando bombas)

    ManagerUI managerUI; // Refer�ncia ao script que gerencia a interface de usu�rio
    GameObject menu, gameOver; // Refer�ncias para os objetos de Menu e Game Over

    // Propriedade para acessar o estado do modo bandeira
    public bool ModoBandeira { get => modoBandeira; }

    // Alterna entre os modos de bandeira (ativa ou desativa)
    public void AlterarModoBandeira()
    {
        modoBandeira = !modoBandeira; // Inverte o estado do modo bandeira
    }

    // M�todo de inicializa��o, configurando a interface e preparando os objetos necess�rios
    private void Start()
    {
        managerUI = GetComponent<ManagerUI>(); // Obt�m a refer�ncia ao script de gerenciamento da interface
        menu = GameObject.Find("Menu Window"); // Encontra o objeto Menu na cena
        gameOver = GameObject.Find("GameOver"); // Encontra o objeto Game Over na cena
    }

    // Define o tamanho do campo (n�mero de c�lulas por lado) e atualiza a barra de progresso com o n�mero de bombas
    public void DefinirDiametro(string value)
    {
        diametroDoCampo = int.Parse(value); // Converte o valor para inteiro
        managerUI.AtualizarBarra((float)numeroDeBombas / (diametroDoCampo * diametroDoCampo)); // Atualiza a barra de progresso com base no n�mero de bombas
    }

    // Define o n�mero de bombas no campo e atualiza a barra de progresso
    public void DefinirNumeroDeBombas(string value)
    {
        numeroDeBombas = int.Parse(value); // Converte o valor para inteiro
        managerUI.AtualizarBarra((float)numeroDeBombas / (diametroDoCampo * diametroDoCampo)); // Atualiza a barra de progresso com base no n�mero de bombas
    }

    // Inicia o jogo, gera o campo minado, coloca as bombas e ajusta a c�mera
    public void IniciarJogo()
    {
        ExcluirCampo(); // Exclui o campo anterior, se houver
        GerarCampoMinado(); // Gera o novo campo minado
        Camera.main.transform.position = new Vector3(diametroDoCampo / 2f - 0.5f, diametroDoCampo / 2f - 0.5f, -10); // Ajusta a posi��o da c�mera
        Camera.main.orthographicSize = diametroDoCampo / 2f; // Ajusta o tamanho da c�mera com base no tamanho do campo

        DistribuirBombas(); // Coloca as bombas no campo
        menu.SetActive(false); // Desativa o menu
        gameOver.SetActive(false); // Desativa a tela de Game Over
    }

    private void DistribuirBombas()
    {
        throw new NotImplementedException();
    }

    // Exclui o campo minado atual (se j� existir) antes de criar um novo
    void ExcluirCampo()
    {
        if (areas != null) // Verifica se a matriz de �reas j� foi criada
        {
            foreach (Area area in areas)
            {
                Destroy(area.gameObject); // Destroi todas as �reas existentes
            }
        }
    }

    // Gera o campo minado (cria uma nova matriz de �reas)
    public void GerarCampoMinado()
    {
        if (numeroDeBombas < Mathf.Pow(diametroDoCampo, 2)) // Verifica se o n�mero de bombas n�o � maior que o n�mero de c�lulas
        {
            areas = new Area[diametroDoCampo, diametroDoCampo]; // Cria uma nova matriz para o campo

            // Cria cada �rea (c�lula) no campo
            for (int i = 0; i < diametroDoCampo; i++)
            {
                for (int j = 0; j < diametroDoCampo; j++)
                {
                    Area area = Instantiate(AreaPrefab, new Vector2(i, j), Quaternion.identity).GetComponent<Area>(); // Instancia o prefab da �rea
                    area.DefinirIndex(i, j); // Define a posi��o da �rea na matriz
                    areas[i, j] = area; // Adiciona a �rea na matriz
                }
            }
        }
    }

    // M�todo para verificar o n�mero de bombas ao redor de uma posi��o (x, y)
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



