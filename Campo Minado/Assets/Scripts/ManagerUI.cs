using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    [SerializeField]
    Image barraDeDificuldade; // Referência à barra de dificuldade (UI)

    [SerializeField] Gradient corDaBarra; // Gradiente de cores para a barra de dificuldade (indica a progressão do jogo)

    [SerializeField] TextMeshProUGUI gameOverText; // Referência ao texto que será atualizado no Game Over (Vitória ou Derrota)

    // Atualiza a barra de progresso da dificuldade
    // 'value' varia de 0 a 1 e é usado para preencher a barra de acordo com a dificuldade do jogo
    public void AtualizarBarra(float value)
    {
        barraDeDificuldade.fillAmount = value; // Ajusta o preenchimento da barra de acordo com o valor
        barraDeDificuldade.color = corDaBarra.Evaluate(value); // Atualiza a cor da barra com base no valor, usando o gradiente
    }

    // Atualiza o texto do Game Over com base na vitória ou derrota
    // 'venceu' é um valor booleano que indica se o jogador ganhou ou perdeu
    public void AtualizarTexto(bool venceu)
    {
        if (venceu)
        {
            gameOverText.text = "Vitória"; // Se venceu, exibe "Vitória"
        }
        else
        {
            gameOverText.text = "Derrota"; // Se perdeu, exibe "Derrota"
        }
    }

    // Método de retorno que retorna o valor atual de preenchimento da barra de dificuldade
    public float RetornarValorBarra()
    {
        return barraDeDificuldade.fillAmount; // Retorna o valor atual de preenchimento da barra (entre 0 e 1)
    }

    // Método de retorno que retorna a cor da barra de dificuldade no momento
    public Color RetornarCorDaBarra()
    {
        return barraDeDificuldade.color; // Retorna a cor da barra de acordo com o gradiente
    }

    // Método de retorno que retorna o texto atual do Game Over (Vitória ou Derrota)
    public string RetornarTextoGameOver()
    {
        return gameOverText.text; // Retorna o texto atual de Game Over
    }
}

