using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] bool bomba; // Indica se a �rea cont�m uma bomba
    public bool revelado, bandeira; // Flags para verificar se a �rea foi revelada ou se h� uma bandeira

    int indexI, indexJ; // �ndices para identificar a posi��o da �rea na grade

    [SerializeField] Sprite[] spritesVazios; // Sprites que representam o n�mero de bombas ao redor, se houver
    [SerializeField] Sprite bombaSprite, bandeiraSprite, spriteOriginal; // Sprites para bomba, bandeira e o estado original da �rea

    // Propriedade para acesso e modifica��o da vari�vel 'bomba'
    public bool Bomba { get => bomba; set => bomba = value; }

    // M�todo chamado no in�cio para armazenar o sprite original da �rea
    private void Start()
    {
        spriteOriginal = GetComponent<SpriteRenderer>().sprite;
    }

    // Define os �ndices 'i' e 'j' que representam a posi��o desta �rea na grade
    public void DefinirIndex(int i, int j)
    {
        indexI = i;
        indexJ = j;
    }

    // M�todo que � chamado quando a �rea � clicada
    public void Clicado()
    {
        if (GameManager.instance.ModoBandeira)
        {
            // Se o modo bandeira estiver ativado, transforma em bandeira
            TransformarBandeira();
        }
        else
        {
            // Caso contr�rio, revela a �rea
            Revelar();
        }
    }

    // Alterna o estado de bandeira na �rea (coloca ou remove a bandeira)
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

    // Revela a �rea, mostrando o n�mero de bombas ao redor ou a bomba, se for o caso
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
                // Mostra o n�mero de bombas ao redor da �rea
                GetComponent<SpriteRenderer>().sprite = spritesVazios[GameManager.instance.ChecarEntorno(indexI, indexJ)];
                // Verifica se o jogador ganhou
                GameManager.instance.ChecarVitoria();
            }
        }
    }

    // Revela a �rea como uma bomba (usado para mostrar a bomba ao final do jogo)
    public void RevelarBomba()
    {
        revelado = true;
        // Altera o sprite para o sprite de bomba
        GetComponent<SpriteRenderer>().sprite = bombaSprite;
    }

    // M�todo de retorno para verificar se a �rea cont�m uma bomba
    public bool RetornarBomba()
    {
        return bomba; // Retorna o valor da vari�vel 'bomba'
    }

    // M�todo de retorno para verificar se a �rea foi revelada
    public bool RetornarRevelado()
    {
        return revelado; // Retorna o valor da vari�vel 'revelado'
    }

    // M�todo de retorno para verificar se a �rea cont�m uma bandeira
    public bool RetornarBandeira()
    {
        return bandeira; // Retorna o valor da vari�vel 'bandeira'
    }
}
