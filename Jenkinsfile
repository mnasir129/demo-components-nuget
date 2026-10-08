@Library('hip-common-lib') _

pipeline {

agent {
    docker {
        image '172.28.52.31:8081/docker-group/local-dotnet9:latest'
        registryUrl 'http://172.28.52.31:8081'
        registryCredentialsId 'nexus-ci'
        reuseNode true
        alwaysPull true
        label 'oracle'
        args '-u 0:0'
    }
}

    options {
        ansiColor('xterm')
        timestamps()
        timeout(time: 2, unit: 'HOURS')
        disableConcurrentBuilds()
    }

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Show Branch') {
            steps {
                sh '''
                    echo "================================"
                    echo "Jenkins branch: $BRANCH_NAME"
                    echo "Git commit:"
                    git rev-parse HEAD
                    echo "================================"
                '''
            }
        }

        stage('Compile') {
            steps {
                sh '''
                    dotnet build -c Release Demo.Components.csproj
                '''
            }
        }

        stage('Package Non-PROD') {
            when {
                branch 'develop'
            }

            steps {
                sh '''
                    echo "This is DEVELOP."
                    echo "Later this stage will publish to nuget-lab-dev."

                    dotnet pack \
                      -c Release \
                      Demo.Components.csproj
                '''
            }
        }

        stage('Package PROD') {
            when {
                branch 'master'
            }

            steps {
                sh '''
                    echo "This is MASTER."
                    echo "Later this stage will publish to nuget-lab-releases."

                    dotnet pack \
                      -c Release \
                      Demo.Components.csproj
                '''
            }
        }
    }

    post {
        always {
            deleteDir()
        }
    }
}
