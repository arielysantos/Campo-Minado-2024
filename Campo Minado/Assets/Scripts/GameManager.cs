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
}
