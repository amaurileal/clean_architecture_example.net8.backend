﻿namespace MotorcycleRental.Domain.Exceptions
{
    public class NotFoundException(string resourceType, string resourceIdentifier) 
        : Exception($"{resourceType} with id: {resourceIdentifier} doesn't exists")
    {
    }
}
