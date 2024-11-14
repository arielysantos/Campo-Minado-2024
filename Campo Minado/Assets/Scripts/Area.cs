using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] bool bomba;  // Indica se a c�lula cont�m uma bomba
    public bool revelado, bandeira; // Se a c�lula foi revelada ou tem uma bandeira

    int indexI, indexJ;  // �ndices que representam a posi��o da c�lula no grid (tabuleiro)

    [SerializeField] Sprite[] spritesVazios; // Sprites usados para c�lulas vazias (dependendo do n�mero de bombas ao redor)
    [SerializeField] Sprite bombaSprite, bandeiraSprite, spriteOriginal; // Sprites para bomba, bandeira e c�lula original

    public bool Bomba { get => bomba; set => bomba = value; } // Getter e setter para o campo bomba

    // Fun��o chamada ao iniciar o jogo
    private void Start()
    {
        // Salva o sprite original da c�lula para poder restaur�-lo mais tarde
        spriteOriginal = GetComponent<SpriteRenderer>().sprite;
    }

    // Fun��o para definir a posi��o (�ndices) da c�lula no grid

    // Fun��o para definir a posi��o (�ndices) da c�lula no grid
    public void DefinirIndex(int i, int j)
    {
        indexI = i;  // Define a posi��o no eixo X (horizontal)
        indexJ = j;  // Define a posi��o no eixo Y (vertical)
    }

    // Fun��o chamada ao clicar na c�lula
    public void Clicado()
    {
        // Se o jogador estiver no modo bandeira, a c�lula ser� marcada com uma bandeira
        if (GameManager.instance.ModoBandeira)
        {
            TransformarBandeira();
        }
        // Caso contr�rio, a c�lula ser� revelada
        else
        {
            Revelar();
        }
    }

    // Fun��o para alternar a presen�a da bandeira na c�lula
    void TransformarBandeira()
    {
        // Se a c�lula n�o tiver bandeira, coloca uma bandeira
        if (!bandeira)
        {
            bandeira = true;  // Marca a c�lula com bandeira
            GetComponent<SpriteRenderer>().sprite = bandeiraSprite;  // Altera o sprite para o de bandeira
        }
        // Se a c�lula j� tiver bandeira, remove a bandeira
        else
        {
            bandeira = false;  // Remove a bandeira
            GetComponent<SpriteRenderer>().sprite = spriteOriginal;  // Restaura o sprite original
        }
    }
}
