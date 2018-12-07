﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities {

	public static float Map(float input, float inMin, float inMax, float outMin, float outMax)
    {
            return (input - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
