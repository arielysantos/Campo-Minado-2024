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
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
}
