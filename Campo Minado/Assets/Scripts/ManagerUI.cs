using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    [SerializeField]
    Image barraDeDificuldade; // Refer�ncia � barra de dificuldade (UI)

    [SerializeField] Gradient corDaBarra; // Gradiente de cores para a barra de dificuldade (indica a progress�o do jogo)

    [SerializeField] TextMeshProUGUI gameOverText; // Refer�ncia ao texto que ser� atualizado no Game Over (Vit�ria ou Derrota)

    // Atualiza a barra de progresso da dificuldade
    // 'value' varia de 0 a 1 e � usado para preencher a barra de acordo com a dificuldade do jogo
    public void AtualizarBarra(float value)
    {
        barraDeDificuldade.fillAmount = value; // Ajusta o preenchimento da barra de acordo com o valor
        barraDeDificuldade.color = corDaBarra.Evaluate(value); // Atualiza a cor da barra com base no valor, usando o gradiente
    }

    // Atualiza o texto do Game Over com base na vit�ria ou derrota
    // 'venceu' � um valor booleano que indica se o jogador ganhou ou perdeu
    public void AtualizarTexto(bool venceu)
    {
        if (venceu)
        {
            gameOverText.text = "Vit�ria"; // Se venceu, exibe "Vit�ria"
        }
        else
        {
            gameOverText.text = "Derrota"; // Se perdeu, exibe "Derrota"
        }
    }

    // M�todo de retorno que retorna o valor atual de preenchimento da barra de dificuldade
    public float RetornarValorBarra()
    {
        return barraDeDificuldade.fillAmount; // Retorna o valor atual de preenchimento da barra (entre 0 e 1)
    }

    // M�todo de retorno que retorna a cor da barra de dificuldade no momento
    public Color RetornarCorDaBarra()
    {
        return barraDeDificuldade.color; // Retorna a cor da barra de acordo com o gradiente
    }

    // M�todo de retorno que retorna o texto atual do Game Over (Vit�ria ou Derrota)
    public string RetornarTextoGameOver()
    {
        return gameOverText.text; // Retorna o texto atual de Game Over
    }
}

