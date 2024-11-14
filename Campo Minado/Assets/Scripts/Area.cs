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
}
