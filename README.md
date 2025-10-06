📮 ComplaintsSolution

Sistema de recebimento e publicação de reclamações via canais digitais e físicos, com arquitetura escalável e processamento assíncrono.

📌 Objetivo
Este projeto faz parte da solução proposta para o case técnico, com foco em:
- Receber reclamações via API
- Validar dados e anexos
- Publicar mensagens em filas (RabbitMQ ou AWS SQS)
- Preparar os dados para processamento assíncrono por Workers

🧱 Arquitetura
O projeto segue os princípios da Clean Architecture e está estruturado em camadas:
- Domain: Entidades e regras de negócio
- Application: Casos de uso e orquestração
- Infrastructure: Persistência, mensageria e serviços externos
- Presentation: Controllers e endpoints REST

🚀 Funcionalidades
- Recebimento de reclamações via POST
- Validação de campos obrigatórios
- Upload de anexos para AWS S3
- Publicação de mensagens em RabbitMQ (canal digital)
- Publicação de documentos em AWS SQS (canal físico)
- Suporte a múltiplas categorias por reclamação

🔗 Integrações
- RabbitMQ: Gerenciamento de mensagens do canal digital
- AWS SQS: Gerenciamento de documentos do canal físico
- AWS S3: Armazenamento de anexos
- Textract (via Worker): Extração de texto de documentos físicos
