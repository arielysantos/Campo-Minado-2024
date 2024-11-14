using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] bool bomba;  // Indica se a célula contém uma bomba
    public bool revelado, bandeira; // Se a célula foi revelada ou tem uma bandeira

    int indexI, indexJ;  // Índices que representam a posição da célula no grid (tabuleiro)

    [SerializeField] Sprite[] spritesVazios; // Sprites usados para células vazias (dependendo do número de bombas ao redor)
    [SerializeField] Sprite bombaSprite, bandeiraSprite, spriteOriginal; // Sprites para bomba, bandeira e célula original

    public bool Bomba { get => bomba; set => bomba = value; } // Getter e setter para o campo bomba

    // Função chamada ao iniciar o jogo
    private void Start()
    {
        // Salva o sprite original da célula para poder restaurá-lo mais tarde
        spriteOriginal = GetComponent<SpriteRenderer>().sprite;
    }

    // Função para definir a posição (índices) da célula no grid

    // Função para definir a posição (índices) da célula no grid
    public void DefinirIndex(int i, int j)
    {
        indexI = i;  // Define a posição no eixo X (horizontal)
        indexJ = j;  // Define a posição no eixo Y (vertical)
    }

    // Função chamada ao clicar na célula
    public void Clicado()
    {
        // Se o jogador estiver no modo bandeira, a célula será marcada com uma bandeira
        if (GameManager.instance.ModoBandeira)
        {
            TransformarBandeira();
        }
        // Caso contrário, a célula será revelada
        else
        {
            Revelar();
        }
    }

    // Função para alternar a presença da bandeira na célula
    void TransformarBandeira()
    {
        // Se a célula não tiver bandeira, coloca uma bandeira
        if (!bandeira)
        {
            bandeira = true;  // Marca a célula com bandeira
            GetComponent<SpriteRenderer>().sprite = bandeiraSprite;  // Altera o sprite para o de bandeira
        }
        // Se a célula já tiver bandeira, remove a bandeira
        else
        {
            bandeira = false;  // Remove a bandeira
            GetComponent<SpriteRenderer>().sprite = spriteOriginal;  // Restaura o sprite original
        }
    }
}
