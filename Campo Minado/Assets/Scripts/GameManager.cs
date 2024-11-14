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
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
}
