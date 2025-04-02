import i18n from 'i18next'
import { initReactI18next } from 'react-i18next'
import LanguageDetector from 'i18next-browser-languagedetector'

import enTranslation from '../locales/en.json'
import esTranslation from '../locales/es.json'

// the translations
const resources = {
  en: {
    translation: enTranslation
  },
  es: {
    translation: esTranslation
  }
}

i18n
// detect user language
  .use(LanguageDetector)
// pass the i18n instance to react-i18next
  .use(initReactI18next)
// init i18next
  .init({
    resources,
    fallbackLng: 'es', // default language
    interpolation: {
      escapeValue: false // react already safes from xss
    }
  })

export default i18n
