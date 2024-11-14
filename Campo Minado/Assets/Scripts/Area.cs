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
}
