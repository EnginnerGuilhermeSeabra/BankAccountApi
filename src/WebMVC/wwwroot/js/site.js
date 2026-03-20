// ========================================
// TES Fintech - Site.js
// ========================================

document.addEventListener('DOMContentLoaded', function () {
    console.log('✅ TES Fintech UI Loaded');

    // ❶ Initialize tooltips
    initializeTooltips();

    // ❷ Setup form validations
    setupFormValidation();

    // ❸ Setup CPF mask
    setupCPFMask();

    // ❹ Setup alert dismiss
    setupAlertDismiss();

    // ❺ Setup scroll animations
    setupScrollAnimations();

    // ❻ Setup loading states
    setupLoadingStates();
});

// ========================================
// 1. Tooltips
// ========================================
function initializeTooltips() {
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
}

// ========================================
// 2. Form Validation
// ========================================
function setupFormValidation() {
    const forms = document.querySelectorAll('form');
    
    forms.forEach(form => {
        form.addEventListener('submit', function (e) {
            if (!form.checkValidity()) {
                e.preventDefault();
                e.stopPropagation();
            }
            form.classList.add('was-validated');
        }, false);
    });
}

// ========================================
// 3. CPF Mask
// ========================================
function setupCPFMask() {
    const cpfInputs = document.querySelectorAll('input[name="Cpf"], input[placeholder*="CPF"]');
    
    cpfInputs.forEach(input => {
        input.addEventListener('input', function (e) {
            // Remove non-numeric characters
            let value = e.target.value.replace(/\D/g, '');
            
            // Limit to 11 digits
            if (value.length > 11) {
                value = value.slice(0, 11);
            }
            
            // Format: XXX.XXX.XXX-XX (optional, only for display)
            if (value.length >= 9) {
                value = value.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
            } else if (value.length >= 6) {
                value = value.replace(/(\d{3})(\d{3})(\d)/, '$1.$2.$3');
            } else if (value.length >= 3) {
                value = value.replace(/(\d{3})(\d)/, '$1.$2');
            }
            
            e.target.value = value;
        });
    });
}

// ========================================
// 4. Alert Auto-Dismiss
// ========================================
function setupAlertDismiss() {
    const alerts = document.querySelectorAll('.alert');
    
    alerts.forEach(alert => {
        // Auto-dismiss success alerts after 5 seconds
        if (alert.classList.contains('alert-success')) {
            setTimeout(() => {
                const bsAlert = new bootstrap.Alert(alert);
                bsAlert.close();
            }, 5000);
        }
    });
}

// ========================================
// 5. Scroll Animations
// ========================================
function setupScrollAnimations() {
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
            }
        });
    }, { threshold: 0.1 });

    const elements = document.querySelectorAll('.card, .alert');
    elements.forEach(el => {
        el.style.opacity = '0';
        el.style.transform = 'translateY(10px)';
        el.style.transition = 'opacity 0.3s ease, transform 0.3s ease';
        observer.observe(el);
    });
}

// ========================================
// 6. Loading States
// ========================================
function setupLoadingStates() {
    const buttons = document.querySelectorAll('button[type="submit"]');

    buttons.forEach(button => {
        button.addEventListener('click', function () {
            const btn = this;
            const originalHTML = this.innerHTML;

            // Defer disabling so the browser can initiate form submission first.
            // Setting disabled=true synchronously in a click handler prevents submission.
            setTimeout(() => {
                btn.disabled = true;
                btn.innerHTML = '<i class="bi bi-hourglass-split"></i> Processando...';
            }, 0);

            // Re-enable after a timeout in case of validation errors or slow response
            setTimeout(() => {
                btn.disabled = false;
                btn.innerHTML = originalHTML;
            }, 3000);
        });
    });
}

// ========================================
// Utility Functions
// ========================================

/**
 * Format CPF for display
 */
function formatCPF(cpf) {
    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
}

/**
 * Show notification
 */
function showNotification(message, type = 'info') {
    const alertClass = {
        'success': 'alert-success',
        'error': 'alert-danger',
        'warning': 'alert-warning',
        'info': 'alert-info'
    }[type] || 'alert-info';

    const alertHTML = `
        <div class="alert ${alertClass} alert-dismissible fade show" role="alert">
            <i class="bi bi-${type === 'error' ? 'exclamation' : 'info'}-circle"></i>
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;

    const container = document.querySelector('.container');
    const tempDiv = document.createElement('div');
    tempDiv.innerHTML = alertHTML;
    
    if (container) {
        container.insertBefore(tempDiv.querySelector('.alert'), container.firstChild);
    }
}

/**
 * Confirm before action
 */
function confirmAction(message) {
    return confirm(message);
}

/**
 * Format date to locale string
 */
function formatDate(date) {
    return new Date(date).toLocaleDateString('pt-BR', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    });
}

// ========================================
// Console Branding
// ========================================
console.log(
    "%c🏦 TES Fintech",
    "color: #667eea; font-size: 20px; font-weight: bold; text-shadow: 2px 2px 4px rgba(0,0,0,0.2);"
);
console.log(
    "%cSistema de Gerenciamento de Contas Bancárias",
    "color: #764ba2; font-size: 12px; margin-top: 5px;"
);
console.log(
    "%c.NET 8 | Bootstrap 5 | MVC Architecture",
    "color: #6b7280; font-size: 11px; margin-top: 5px;"
);
