﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Pickup();
    void Drop();
    string GetTitle();
}
