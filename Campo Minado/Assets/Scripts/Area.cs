using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] bool bomba; // Indica se a área contém uma bomba
    public bool revelado, bandeira; // Flags para verificar se a área foi revelada ou se há uma bandeira

    int indexI, indexJ; // Índices para identificar a posição da área na grade

    [SerializeField] Sprite[] spritesVazios; // Sprites que representam o número de bombas ao redor, se houver
    [SerializeField] Sprite bombaSprite, bandeiraSprite, spriteOriginal; // Sprites para bomba, bandeira e o estado original da área

    // Propriedade para acesso e modificação da variável 'bomba'
    public bool Bomba { get => bomba; set => bomba = value; }

    // Método chamado no início para armazenar o sprite original da área
    private void Start()
    {
        spriteOriginal = GetComponent<SpriteRenderer>().sprite;
    }

    // Define os índices 'i' e 'j' que representam a posição desta área na grade
    public void DefinirIndex(int i, int j)
    {
        indexI = i;
        indexJ = j;
    }

    // Método que é chamado quando a área é clicada
    public void Clicado()
    {
        if (GameManager.instance.ModoBandeira)
        {
            // Se o modo bandeira estiver ativado, transforma em bandeira
            TransformarBandeira();
        }
        else
        {
            // Caso contrário, revela a área
            Revelar();
        }
    }

    // Alterna o estado de bandeira na área (coloca ou remove a bandeira)
    void TransformarBandeira()
    {
        if (!bandeira)
        {
            bandeira = true;
            // Altera o sprite para a bandeira
            GetComponent<SpriteRenderer>().sprite = bandeiraSprite;
        }
        else
        {
            bandeira = false;
            // Restaura o sprite original
            GetComponent<SpriteRenderer>().sprite = spriteOriginal;
        }
    }

    // Revela a área, mostrando o número de bombas ao redor ou a bomba, se for o caso
    void Revelar()
    {
        if (!revelado && !bandeira)
        {
            if (bomba)
            {
                // Se houver bomba, o jogo termina
                GameManager.instance.GameOver();
            }
            else
            {
                revelado = true;
                // Mostra o número de bombas ao redor da área
                GetComponent<SpriteRenderer>().sprite = spritesVazios[GameManager.instance.ChecarEntorno(indexI, indexJ)];
                // Verifica se o jogador ganhou
                GameManager.instance.ChecarVitoria();
            }
        }
    }

    // Revela a área como uma bomba (usado para mostrar a bomba ao final do jogo)
    public void RevelarBomba()
    {
        revelado = true;
        // Altera o sprite para o sprite de bomba
        GetComponent<SpriteRenderer>().sprite = bombaSprite;
    }

    // Método de retorno para verificar se a área contém uma bomba
    public bool RetornarBomba()
    {
        return bomba; // Retorna o valor da variável 'bomba'
    }

    // Método de retorno para verificar se a área foi revelada
    public bool RetornarRevelado()
    {
        return revelado; // Retorna o valor da variável 'revelado'
    }

    // Método de retorno para verificar se a área contém uma bandeira
    public bool RetornarBandeira()
    {
        return bandeira; // Retorna o valor da variável 'bandeira'
    }
}
