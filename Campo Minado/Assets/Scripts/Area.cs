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

    // Método para revelar a célula
    void Revelar()
    {
        // Só revela a célula se ela ainda não foi revelada e não está com bandeira
        if (!revelado && !bandeira)
        {
            if (bomba)  // Se a célula contém uma bomba, o jogo acaba
            {
                GameManager.instance.GameOver();  // Chama o GameOver no GameManager
            }
            else  // Se a célula não contém bomba, revela o conteúdo
            {
                revelado = true;  // Marca a célula como revelada
                // Atualiza o sprite da célula com o número de bombas ao redor, obtido do GameManager
                GetComponent<SpriteRenderer>().sprite = spritesVazios[GameManager.instance.ChecarEntorno(indexI, indexJ)];

                // Se a célula tem 0 bombas ao redor (uma célula vazia), revela também as vizinhas
                if (GameManager.instance.ChecarEntorno(indexI, indexJ) == 0)
                {
                    RevelarVizinhas(indexI, indexJ);  // Chama o método que revela as células vizinhas
                }

                GameManager.instance.ChecarVitoria();  // Verifica se o jogador venceu o jogo
            }
        }
    }

    // Método para revelar as células vizinhas ao redor de uma célula que tem 0 bombas adjacentes
    void RevelarVizinhas(int i, int j)
    {
        // Laço para percorrer as células vizinhas (em uma matriz 3x3 ao redor da célula clicada)
        for (int di = -1; di <= 1; di++)  // Laço para as linhas vizinhas (-1, 0, +1)
        {
            for (int dj = -1; dj <= 1; dj++)  // Laço para as colunas vizinhas (-1, 0, +1)
            {
                int novoI = i + di;  // Calcula o novo índice X (linha)
                int novoJ = j + dj;  // Calcula o novo índice Y (coluna)

                // Verifica se a célula vizinha está dentro dos limites do campo de jogo
                if (novoI >= 0 && novoI < GameManager.instance.Largura && novoJ >= 0 && novoJ < GameManager.instance.Altura)
                {
                    Area vizinha = GameManager.instance.GetArea(novoI, novoJ);  // Obtém a referência para a célula vizinha

                    // Só revela a célula vizinha se ela ainda não foi revelada e não estiver com bandeira
                    if (!vizinha.revelado && !vizinha.bandeira)
                    {
                        vizinha.Revelar();  // Revela a célula vizinha (recursivamente chama Revelar)
                    }
                }
            }
        }
    }
}
