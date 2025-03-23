﻿namespace PNA.Core.ValueObjects;

public record Address (
    string Street,
    string City,
    string State,
    string ZipCode
    );