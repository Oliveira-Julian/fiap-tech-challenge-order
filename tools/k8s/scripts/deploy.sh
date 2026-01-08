#!/bin/bash

set -e

echo "📁 Acessando raiz da pasta K8s..."
cd "$(dirname "$0")"
cd ..

NAMESPACE="fiap-tech-challenge"

echo "🚀 Aplicando Namespace..."
kubectl apply -f api/food-challenge-ns.yaml

echo "🔍 Verificando se o Namespace foi criado:"
kubectl get ns "$NAMESPACE"

echo "🧾 Aplicando ConfigMaps e Secrets (Postgres + API)..."
kubectl apply -f postgres/postgres-configmap.yaml
kubectl apply -f postgres/postgres-init-sql-configmap.yaml
kubectl apply -f api/food-challenge-configmap.yaml

kubectl apply -f postgres/postgres-secrets.yaml
kubectl apply -f api/food-challenge-secrets.yaml

echo "🐘 Aplicando PostgreSQL (Service + StatefulSet)..."
kubectl apply -f postgres/postgres-service.yaml
kubectl apply -f postgres/postgres-st.yaml

echo "⏳ Aguardando PostgreSQL ficar pronto..."
kubectl wait --for=condition=ready pod -l app=postgres -n "$NAMESPACE" --timeout=90s

echo "🌐 Aplicando API (Service + HPA + Deployment + Ingress)..."
kubectl apply -f api/food-challenge-service.yaml
kubectl apply -f api/food-challenge-hpa.yaml
kubectl apply -f api/food-challenge-deployment.yaml
kubectl apply -f api/food-challenge-ingress.yaml

echo "⏳ Aguardando a API ficar pronta..."
kubectl wait --for=condition=ready pod -l app=food-challenge-api -n "$NAMESPACE" --timeout=90s
