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

    // M�todo para revelar a c�lula
    void Revelar()
    {
        // S� revela a c�lula se ela ainda n�o foi revelada e n�o est� com bandeira
        if (!revelado && !bandeira)
        {
            if (bomba)  // Se a c�lula cont�m uma bomba, o jogo acaba
            {
                GameManager.instance.GameOver();  // Chama o GameOver no GameManager
            }
            else  // Se a c�lula n�o cont�m bomba, revela o conte�do
            {
                revelado = true;  // Marca a c�lula como revelada
                // Atualiza o sprite da c�lula com o n�mero de bombas ao redor, obtido do GameManager
                GetComponent<SpriteRenderer>().sprite = spritesVazios[GameManager.instance.ChecarEntorno(indexI, indexJ)];

                // Se a c�lula tem 0 bombas ao redor (uma c�lula vazia), revela tamb�m as vizinhas
                if (GameManager.instance.ChecarEntorno(indexI, indexJ) == 0)
                {
                    RevelarVizinhas(indexI, indexJ);  // Chama o m�todo que revela as c�lulas vizinhas
                }

                GameManager.instance.ChecarVitoria();  // Verifica se o jogador venceu o jogo
            }
        }
    }

    // M�todo para revelar as c�lulas vizinhas ao redor de uma c�lula que tem 0 bombas adjacentes
    void RevelarVizinhas(int i, int j)
    {
        // La�o para percorrer as c�lulas vizinhas (em uma matriz 3x3 ao redor da c�lula clicada)
        for (int di = -1; di <= 1; di++)  // La�o para as linhas vizinhas (-1, 0, +1)
        {
            for (int dj = -1; dj <= 1; dj++)  // La�o para as colunas vizinhas (-1, 0, +1)
            {
                int novoI = i + di;  // Calcula o novo �ndice X (linha)
                int novoJ = j + dj;  // Calcula o novo �ndice Y (coluna)

                // Verifica se a c�lula vizinha est� dentro dos limites do campo de jogo
                if (novoI >= 0 && novoI < GameManager.instance.Largura && novoJ >= 0 && novoJ < GameManager.instance.Altura)
                {
                    Area vizinha = GameManager.instance.GetArea(novoI, novoJ);  // Obt�m a refer�ncia para a c�lula vizinha

                    // S� revela a c�lula vizinha se ela ainda n�o foi revelada e n�o estiver com bandeira
                    if (!vizinha.revelado && !vizinha.bandeira)
                    {
                        vizinha.Revelar();  // Revela a c�lula vizinha (recursivamente chama Revelar)
                    }
                }
            }
        }
    }
}
