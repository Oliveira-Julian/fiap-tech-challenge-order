#!/bin/bash

set -e

echo "📁 Acessando raiz da pasta K8s..."
cd "$(dirname "$0")"
cd ..

NAMESPACE="fiap-tech-challenge"

echo "⚠️ Apagando TODOS os recursos do namespace: $NAMESPACE"

# Apaga todos os recursos dentro do namespace (exceto o namespace em si)
kubectl delete all --all -n "$NAMESPACE" --ignore-not-found
kubectl delete pvc --all -n "$NAMESPACE" --ignore-not-found
kubectl delete pv --all -n "$NAMESPACE" --ignore-not-found
kubectl delete configmap --all -n "$NAMESPACE" --ignore-not-found
kubectl delete secret --all -n "$NAMESPACE" --ignore-not-found
kubectl delete hpa --all -n "$NAMESPACE" --ignore-not-found
kubectl delete job --all -n "$NAMESPACE" --ignore-not-found
kubectl delete statefulset --all -n "$NAMESPACE" --ignore-not-found
kubectl delete service --all -n "$NAMESPACE" --ignore-not-found
kubectl delete deployment --all -n "$NAMESPACE" --ignore-not-found
kubectl delete ingress --all -n "$NAMESPACE" --ignore-not-found

echo "🗑️ Removendo o próprio namespace..."
kubectl delete namespace "$NAMESPACE" --ignore-not-found

echo "✅ Remoção completa."
