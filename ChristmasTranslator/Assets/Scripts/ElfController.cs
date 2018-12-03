using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class ElfController : MonoBehaviour
    {
        private int currentSacrificeNumber = 0;
        public GameObject[] elves = new GameObject[2];

        public void SacrificeNextLayer()
        {
            elves[currentSacrificeNumber].SetActive(false);
            currentSacrificeNumber++;
        }

    }
}
